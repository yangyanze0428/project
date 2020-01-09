/*
  Highcharts JS v7.0.3 (2019-02-06)
 Force directed graph module

 (c) 2010-2019 Torstein Honsi

 License: www.highcharts.com/license
*/
(function(l){"object"===typeof module&&module.exports?(l["default"]=l,module.exports=l):"function"===typeof define&&define.amd?define(function(){return l}):l("undefined"!==typeof Highcharts?Highcharts:void 0)})(function(l){(function(g){g.NodesMixin={createNode:function(k){function a(a,b){return g.find(a,function(a){return a.id===b})}var b=a(this.nodes,k),h=this.pointClass,d;b||(d=this.options.nodes&&a(this.options.nodes,k),b=(new h).init(this,g.extend({className:"highcharts-node",isNode:!0,id:k,y:1},
d)),b.linksTo=[],b.linksFrom=[],b.formatPrefix="node",b.name=b.name||b.options.id,b.getSum=function(){var a=0,c=0;b.linksTo.forEach(function(b){a+=b.weight});b.linksFrom.forEach(function(a){c+=a.weight});return Math.max(a,c)},b.offset=function(a,c){for(var e=0,f=0;f<b[c].length;f++){if(b[c][f]===a)return e;e+=b[c][f].weight}},b.hasShape=function(){var a=0;b.linksTo.forEach(function(b){b.outgoing&&a++});return!b.linksTo.length||a!==b.linksTo.length},this.nodes.push(b));return b}}})(l);(function(g){var k=
g.pick;g.layouts={"reingold-fruchterman":function(a){this.options=a;this.nodes=[];this.links=[];this.series=[];this.box={x:0,y:0,width:0,height:0};this.setInitialRendering(!0)}};g.extend(g.layouts["reingold-fruchterman"].prototype,{run:function(){function a(){b.applyBarycenterForces();b.applyRepulsiveForces();b.applyAttractiveForces();b.applyLimits(b.temperature);b.temperature-=b.diffTemperature;b.prevSystemTemperature=b.systemTemperature;b.systemTemperature=b.getSystemTemperature();d.enableSimulation&&
(h.forEach(function(a){a.render()}),b.maxIterations--&&!b.isStable()?b.simulation=g.win.requestAnimationFrame(a):b.simulation=!1)}var b=this,h=this.series,d=this.options;b.initialRendering&&(b.initPositions(),h.forEach(function(a){a.render()}));b.setK();b.resetSimulation(d);if(d.enableSimulation)b.simulation=g.win.requestAnimationFrame(a);else{for(;b.maxIterations--&&!b.isStable();)a();h.forEach(function(a){a.render()})}},stop:function(){this.simulation&&g.win.cancelAnimationFrame(this.simulation)},
setArea:function(a,b,h,d){this.box={left:a,top:b,width:h,height:d}},setK:function(){this.k=this.options.linkLength||Math.pow(this.box.width*this.box.height/this.nodes.length,.4)},addNodes:function(a){a.forEach(function(a){-1===this.nodes.indexOf(a)&&this.nodes.push(a)},this)},removeNode:function(a){a=this.nodes.indexOf(a);-1!==a&&this.nodes.splice(a,1)},removeLink:function(a){a=this.links.indexOf(a);-1!==a&&this.links.splice(a,1)},addLinks:function(a){a.forEach(function(a){-1===this.links.indexOf(a)&&
this.links.push(a)},this)},addSeries:function(a){-1===this.series.indexOf(a)&&this.series.push(a)},clear:function(){this.nodes.length=0;this.links.length=0;this.series.length=0;this.resetSimulation()},resetSimulation:function(){this.forcedStop=!1;this.systemTemperature=0;this.setMaxIterations();this.setTemperature();this.setDiffTemperature()},setMaxIterations:function(a){this.maxIterations=k(a,this.options.maxIterations)},setTemperature:function(){this.temperature=Math.sqrt(this.nodes.length)},setDiffTemperature:function(){this.diffTemperature=
this.temperature/(this.options.maxIterations+1)},setInitialRendering:function(a){this.initialRendering=a},initPositions:function(){var a=this.options.initialPositions;g.isFunction(a)?a.call(this):"circle"===a?this.setCircularPositions():this.setRandomPositions()},setCircularPositions:function(){function a(b){b.linksFrom.forEach(function(f){c[f.toNode.id]||(c[f.toNode.id]=!0,e.push(f.toNode),a(f.toNode))})}var b=this.box,h=this.nodes,d=2*Math.PI/(h.length+1),e=[],c={};h.filter(function(a){return 0===
a.linksTo.length}).forEach(function(b){e.push(b);a(b)});e.length?h.forEach(function(a){-1===e.indexOf(a)&&e.push(a)}):e=h;e.forEach(function(a,f){a.plotX=k(a.plotX,b.width/2+Math.cos(f*d));a.plotY=k(a.plotY,b.height/2+Math.sin(f*d));a.dispX=0;a.dispY=0})},setRandomPositions:function(){function a(a){a=a*a/Math.PI;return a-=Math.floor(a)}var b=this.box,h=this.nodes,d=h.length+1;h.forEach(function(e,c){e.plotX=k(e.plotX,b.width*a(c));e.plotY=k(e.plotY,b.height*a(d+c));e.dispX=0;e.dispY=0})},applyBarycenterForces:function(){var a=
this.nodes.length,b=this.options.gravitationalConstant,h=0,d=0;this.nodes.forEach(function(a){h+=a.plotX;d+=a.plotY});this.barycenter={x:h,y:d};this.nodes.forEach(function(e){var c=e.getDegree(),c=c*(1+c/2);e.dispX=(h/a-e.plotX)*b*c;e.dispY=(d/a-e.plotY)*b*c})},applyRepulsiveForces:function(){var a=this,b=a.nodes,h=a.options,d=this.k;b.forEach(function(e){b.forEach(function(b){var c,f;e===b||e.fixedPosition||(f=a.getDistXY(e,b),c=a.vectorLength(f),0!==c&&(b=h.repulsiveForce.call(a,c,d),e.dispX+=f.x/
c*b,e.dispY+=f.y/c*b))})})},applyAttractiveForces:function(){var a=this,b=this.options,h=this.k;a.links.forEach(function(d){if(d.fromNode&&d.toNode){var e=a.getDistXY(d.fromNode,d.toNode),c=a.vectorLength(e),g=b.attractiveForce.call(a,c,h);0!==c&&(d.fromNode.fixedPosition||(d.fromNode.dispX-=e.x/c*g,d.fromNode.dispY-=e.y/c*g),d.toNode.fixedPosition||(d.toNode.dispX+=e.x/c*g,d.toNode.dispY+=e.y/c*g))}})},applyLimits:function(a){var b=this,g=b.options,d=b.box,e;b.nodes.forEach(function(c){c.fixedPosition||
(c.dispX+=g.friction*c.dispX,c.dispY+=g.friction*c.dispY,e=c.temperature=b.vectorLength({x:c.dispX,y:c.dispY}),0!==e&&(c.plotX+=c.dispX/e*Math.min(Math.abs(c.dispX),a),c.plotY+=c.dispY/e*Math.min(Math.abs(c.dispY),a)),c.plotX=Math.round(Math.max(Math.min(c.plotX,d.width),d.left)),c.plotY=Math.round(Math.max(Math.min(c.plotY,d.height),d.top)),c.dispX=0,c.dispY=0)})},isStable:function(){return 0===Math.abs(this.systemTemperature-this.prevSystemTemperature)},getSystemTemperature:function(){return this.nodes.reduce(function(a,
b){return a+b.temperature},0)},vectorLength:function(a){return Math.sqrt(a.x*a.x+a.y*a.y)},getDistR:function(a,b){a=this.getDistXY(a,b);return Math.sqrt(a.x*a.x+a.y*a.y)},getDistXY:function(a,b){var g=a.plotX-b.plotX;a=a.plotY-b.plotY;return{x:g,y:a,absX:Math.abs(g),absY:Math.abs(a)}}})})(l);(function(g){var k=g.addEvent,a=g.defined,b=g.seriesType,h=g.seriesTypes,d=g.pick,e=g.Chart,c=g.Point,l=g.Series;b("networkgraph","line",{marker:{enabled:!0},dataLabels:{format:"{key}"},link:{color:"rgba(100, 100, 100, 0.5)",
width:1},draggable:!0,layoutAlgorithm:{initialPositions:"circle",enableSimulation:!1,type:"reingold-fruchterman",maxIterations:1E3,gravitationalConstant:.0625,friction:-.981,repulsiveForce:function(a,b){return b*b/a},attractiveForce:function(a,b){return a*a/b}},showInLegend:!1},{isNetworkgraph:!0,drawGraph:null,isCartesian:!1,requireSorting:!1,directTouch:!0,noSharedTooltip:!0,trackerGroups:["group","markerGroup","dataLabelsGroup"],drawTracker:g.TrackerMixin.drawTrackerPoint,animate:null,createNode:g.NodesMixin.createNode,
generatePoints:function(){var b={},c=this.chart;g.Series.prototype.generatePoints.call(this);this.nodes||(this.nodes=[]);this.colorCounter=0;this.nodes.forEach(function(a){a.linksFrom.length=0;a.linksTo.length=0});this.points.forEach(function(f){a(f.from)&&(b[f.from]||(b[f.from]=this.createNode(f.from)),b[f.from].linksFrom.push(f),f.fromNode=b[f.from],c.styledMode?f.colorIndex=d(f.options.colorIndex,b[f.from].colorIndex):f.color=f.options.color||b[f.from].color);a(f.to)&&(b[f.to]||(b[f.to]=this.createNode(f.to)),
b[f.to].linksTo.push(f),f.toNode=b[f.to]);f.name=f.name||f.id},this);this.options.nodes&&this.options.nodes.forEach(function(a){b[a.id]||(b[a.id]=this.createNode(a.id))},this)},translate:function(){this.processedXData||this.processData();this.generatePoints();this.deferLayout();this.nodes.forEach(function(a){a.isInside=!0;a.linksFrom.forEach(function(a){a.shapeType="path";a.y=1})})},deferLayout:function(){var b=this.options.layoutAlgorithm,c=this.chart.graphLayoutsStorage,e=this.chart.options.chart,
d;this.visible&&(c||(this.chart.graphLayoutsStorage=c={}),d=c[b.type],d||(b.enableSimulation=a(e.forExport)?!e.forExport:b.enableSimulation,c[b.type]=d=new g.layouts[b.type](b)),this.layout=d,d.setArea(0,0,this.chart.plotWidth,this.chart.plotHeight),d.addSeries(this),d.addNodes(this.nodes),d.addLinks(this.points))},render:function(){var a=this.points,b=this.chart.hoverPoint,c=[];this.points=this.nodes;h.line.prototype.render.call(this);this.points=a;a.forEach(function(a){a.fromNode&&a.toNode&&(a.renderLink(),
a.redrawLink())});b&&b.series===this&&this.redrawHalo(b);this.nodes.forEach(function(a){a.dataLabel&&c.push(a.dataLabel)});g.Chart.prototype.hideOverlappingLabels(c)},redrawHalo:function(a){a&&this.halo&&this.halo.attr({d:a.haloPath(this.options.states.hover.halo.size)})},onMouseDown:function(a,b){b=this.chart.pointer.normalize(b);a.fixedPosition={chartX:b.chartX,chartY:b.chartY,plotX:a.plotX,plotY:a.plotY}},onMouseMove:function(a,b){if(a.fixedPosition){var f=this.chart,c=f.pointer.normalize(b);b=
a.fixedPosition.chartX-c.chartX;c=a.fixedPosition.chartY-c.chartY;if(5<Math.abs(b)||5<Math.abs(c))b=a.fixedPosition.plotX-b,c=a.fixedPosition.plotY-c,f.isInsidePlot(b,c)&&(a.plotX=b,a.plotY=c,this.redrawHalo(),this.layout.simulation?this.layout.resetSimulation():(this.layout.enableSimulation||this.layout.setMaxIterations(1),this.layout.setInitialRendering(!1),this.layout.run(),this.layout.setInitialRendering(!0)))}},onMouseUp:function(a){a.fixedPosition&&(this.layout.run(),delete a.fixedPosition)},
destroy:function(){this.nodes.forEach(function(a){a.destroy()});return l.prototype.destroy.apply(this,arguments)}},{getDegree:function(){var a=this.isNode?this.linksFrom.length+this.linksTo.length:0;return 0===a?1:a},getLinkAttribues:function(){var a=this.series.options.link,b=this.options;return{"stroke-width":d(b.width,a.width),stroke:b.color||a.color,dashstyle:b.dashStyle||a.dashStyle}},renderLink:function(){this.graphic||(this.graphic=this.series.chart.renderer.path(this.getLinkPath(this.fromNode,
this.toNode)).attr(this.getLinkAttribues()).add(this.series.group))},redrawLink:function(){this.graphic&&this.graphic.animate({d:this.getLinkPath(this.fromNode,this.toNode)})},getLinkPath:function(a,b){return["M",a.plotX,a.plotY,"L",b.plotX,b.plotY]},destroy:function(){this.isNode&&this.linksFrom.forEach(function(a){a.graphic&&(a.graphic=a.graphic.destroy())});return c.prototype.destroy.apply(this,arguments)}});k(h.networkgraph,"updatedData",function(){this.layout&&this.layout.stop()});k(h.networkgraph.prototype.pointClass,
"remove",function(){this.series.layout&&(this.isNode?this.series.layout.removeNode(this):this.series.layout.removeLink(this))});k(e,"predraw",function(){this.graphLayoutsStorage&&g.objectEach(this.graphLayoutsStorage,function(a){a.stop()})});k(e,"render",function(){this.graphLayoutsStorage&&(g.setAnimation(!1,this),g.objectEach(this.graphLayoutsStorage,function(a){a.run()}))});k(h.networkgraph.prototype.pointClass,"mouseOver",function(){g.css(this.series.chart.container,{cursor:"move"})});k(h.networkgraph.prototype.pointClass,
"mouseOut",function(){g.css(this.series.chart.container,{cursor:"default"})});k(e,"load",function(){var a=this,b=[];a.container&&b.push(k(a.container,"mousedown",function(c){var d=a.hoverPoint;d&&d.series&&d.series.isNetworkgraph&&d.series.options.draggable&&(d.series.onMouseDown(d,c),b.push(k(a.container,"mousemove",function(a){return d.series.onMouseMove(d,a)})),b.push(k(a.container.ownerDocument,"mouseup",function(a){return d.series.onMouseUp(d,a)})))}));k(a,"destroy",function(){b.forEach(function(a){a()})})})})(l)});
//# sourceMappingURL=networkgraph.js.map