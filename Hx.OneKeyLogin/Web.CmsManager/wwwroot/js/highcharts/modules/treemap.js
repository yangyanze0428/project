/*
 Highcharts JS v7.0.3 (2019-02-06)

 (c) 2014-2019 Highsoft AS
 Authors: Jon Arild Nygard / Oystein Moseng

 License: www.highcharts.com/license
*/
(function(m){"object"===typeof module&&module.exports?(m["default"]=m,module.exports=m):"function"===typeof define&&define.amd?define(function(){return m}):m("undefined"!==typeof Highcharts?Highcharts:void 0)})(function(m){var F=function(c){var w=c.extend,m=c.isArray,l=c.isObject,C=c.isNumber,r=c.merge,A=c.pick;return{getColor:function(p,e){var u=e.index,g=e.mapOptionsToLevel,l=e.parentColor,y=e.parentColorIndex,B=e.series,v=e.colors,m=e.siblings,h=B.points,q=B.chart.options.chart,z,w,x,r;if(p){h=
h[p.i];p=g[p.level]||{};if(g=h&&p.colorByPoint)w=h.index%(v?v.length:q.colorCount),z=v&&v[w];if(!B.chart.styledMode){v=h&&h.options.color;q=p&&p.color;if(x=l)x=(x=p&&p.colorVariation)&&"brightness"===x.key?c.color(l).brighten(u/m*x.to).get():l;x=A(v,q,z,x,B.color)}r=A(h&&h.options.colorIndex,p&&p.colorIndex,w,y,e.colorIndex)}return{color:x,colorIndex:r}},getLevelOptions:function(c){var e=null,u,g,p,y;if(l(c))for(e={},p=C(c.from)?c.from:1,y=c.levels,g={},u=l(c.defaults)?c.defaults:{},m(y)&&(g=y.reduce(function(c,
e){var g,h;l(e)&&C(e.level)&&(h=r({},e),g="boolean"===typeof h.levelIsConstant?h.levelIsConstant:u.levelIsConstant,delete h.levelIsConstant,delete h.level,e=e.level+(g?0:p-1),l(c[e])?w(c[e],h):c[e]=h);return c},{})),y=C(c.to)?c.to:1,c=0;c<=y;c++)e[c]=r({},u,l(g[c])?g[c]:{});return e},setTreeValues:function e(c,g){var l=g.before,u=g.idRoot,m=g.mapIdToNode[u],v=g.points[c.i],r=v&&v.options||{},h=0,q=[];w(c,{levelDynamic:c.level-(("boolean"===typeof g.levelIsConstant?g.levelIsConstant:1)?0:m.level),
name:A(v&&v.name,""),visible:u===c.id||("boolean"===typeof g.visible?g.visible:!1)});"function"===typeof l&&(c=l(c,g));c.children.forEach(function(l,u){var m=w({},g);w(m,{index:u,siblings:c.children.length,visible:c.visible});l=e(l,m);q.push(l);l.visible&&(h+=l.val)});c.visible=0<h||c.visible;l=A(r.value,h);w(c,{children:q,childrenTotal:h,isLeaf:c.visible&&!h,val:l});return c},updateRootId:function(c){var e;l(c)&&(e=l(c.options)?c.options:{},e=A(c.rootNode,e.rootId,""),l(c.userOptions)&&(c.userOptions.rootId=
e),c.rootNode=e);return e}}}(m);(function(c,m){var w=c.seriesType,l=c.seriesTypes,C=c.addEvent,r=c.merge,A=c.extend,p=c.error,e=c.defined,u=c.noop,g=c.fireEvent,F=m.getColor,y=m.getLevelOptions,B=c.isArray,v=c.isNumber,H=c.isObject,h=c.isString,q=c.pick,z=c.Series,I=c.stableSort,x=c.Color,J=function(a,b,d){d=d||this;c.objectEach(a,function(n,f){b.call(d,n,f,a)})},E=function(a,b,d){d=d||this;a=b.call(d,a);!1!==a&&E(a,b,d)},K=m.updateRootId;w("treemap","scatter",{allowTraversingTree:!1,showInLegend:!1,
marker:!1,colorByPoint:!1,dataLabels:{enabled:!0,defer:!1,verticalAlign:"middle",formatter:function(){var a=this&&this.point?this.point:{};return h(a.name)?a.name:""},inside:!0},tooltip:{headerFormat:"",pointFormat:"\x3cb\x3e{point.name}\x3c/b\x3e: {point.value}\x3cbr/\x3e"},ignoreHiddenPoint:!0,layoutAlgorithm:"sliceAndDice",layoutStartingDirection:"vertical",alternateStartingDirection:!1,levelIsConstant:!0,drillUpButton:{position:{align:"right",x:-10,y:10}},traverseUpButton:{position:{align:"right",
x:-10,y:10}},borderColor:"#e6e6e6",borderWidth:1,opacity:.15,states:{hover:{borderColor:"#999999",brightness:l.heatmap?0:.1,halo:!1,opacity:.75,shadow:!1}}},{pointArrayMap:["value"],directTouch:!0,optionalAxis:"colorAxis",getSymbol:u,parallelArrays:["x","y","value","colorValue"],colorKey:"colorValue",trackerGroups:["group","dataLabelsGroup"],getListOfParents:function(a,b){a=B(a)?a:[];var d=B(b)?b:[];b=a.reduce(function(a,b,d){b=q(b.parent,"");void 0===a[b]&&(a[b]=[]);a[b].push(d);return a},{"":[]});
J(b,function(a,b,c){""!==b&&-1===d.indexOf(b)&&(a.forEach(function(a){c[""].push(a)}),delete c[b])});return b},getTree:function(){var a=this.data.map(function(a){return a.id}),a=this.getListOfParents(this.data,a);this.nodeMap=[];return this.buildNode("",-1,0,a,null)},init:function(a,b){var d=c.colorSeriesMixin;c.colorSeriesMixin&&(this.translateColors=d.translateColors,this.colorAttribs=d.colorAttribs,this.axisTypes=d.axisTypes);C(this,"setOptions",function(a){a=a.userOptions;e(a.allowDrillToNode)&&
!e(a.allowTraversingTree)&&(a.allowTraversingTree=a.allowDrillToNode,delete a.allowDrillToNode);e(a.drillUpButton)&&!e(a.traverseUpButton)&&(a.traverseUpButton=a.drillUpButton,delete a.drillUpButton)});z.prototype.init.call(this,a,b);this.options.allowTraversingTree&&C(this,"click",this.onClickDrillToNode)},buildNode:function(a,b,d,c,f){var n=this,t=[],k=n.points[b],G=0,e;(c[a]||[]).forEach(function(b){e=n.buildNode(n.points[b].id,b,d+1,c,a);G=Math.max(e.height+1,G);t.push(e)});b={id:a,i:b,children:t,
height:G,level:d,parent:f,visible:!1};n.nodeMap[b.id]=b;k&&(k.node=b);return b},setTreeValues:function(a){var b=this,d=b.options,c=b.nodeMap[b.rootNode],d="boolean"===typeof d.levelIsConstant?d.levelIsConstant:!0,f=0,D=[],t,k=b.points[a.i];a.children.forEach(function(a){a=b.setTreeValues(a);D.push(a);a.ignore||(f+=a.val)});I(D,function(a,b){return a.sortIndex-b.sortIndex});t=q(k&&k.options.value,f);k&&(k.value=t);A(a,{children:D,childrenTotal:f,ignore:!(q(k&&k.visible,!0)&&0<t),isLeaf:a.visible&&
!f,levelDynamic:a.level-(d?0:c.level),name:q(k&&k.name,""),sortIndex:q(k&&k.sortIndex,-t),val:t});return a},calculateChildrenAreas:function(a,b){var d=this,c=d.options,f=d.mapOptionsToLevel[a.level+1],D=q(d[f&&f.layoutAlgorithm]&&f.layoutAlgorithm,c.layoutAlgorithm),t=c.alternateStartingDirection,k=[];a=a.children.filter(function(a){return!a.ignore});f&&f.layoutStartingDirection&&(b.direction="vertical"===f.layoutStartingDirection?0:1);k=d[D](b,a);a.forEach(function(a,c){c=k[c];a.values=r(c,{val:a.childrenTotal,
direction:t?1-b.direction:b.direction});a.pointValues=r(c,{x:c.x/d.axisRatio,width:c.width/d.axisRatio});a.children.length&&d.calculateChildrenAreas(a,a.values)})},setPointValues:function(){var a=this,b=a.xAxis,d=a.yAxis;a.points.forEach(function(c){var f=c.node,n=f.pointValues,t,k,e=0;a.chart.styledMode||(e=(a.pointAttribs(c)["stroke-width"]||0)%2/2);n&&f.visible?(f=Math.round(b.translate(n.x,0,0,0,1))-e,t=Math.round(b.translate(n.x+n.width,0,0,0,1))-e,k=Math.round(d.translate(n.y,0,0,0,1))-e,n=
Math.round(d.translate(n.y+n.height,0,0,0,1))-e,c.shapeType="rect",c.shapeArgs={x:Math.min(f,t),y:Math.min(k,n),width:Math.abs(t-f),height:Math.abs(n-k)},c.plotX=c.shapeArgs.x+c.shapeArgs.width/2,c.plotY=c.shapeArgs.y+c.shapeArgs.height/2):(delete c.plotX,delete c.plotY)})},setColorRecursive:function(a,b,d,c,f){var n=this,e=n&&n.chart,e=e&&e.options&&e.options.colors,k;if(a){k=F(a,{colors:e,index:c,mapOptionsToLevel:n.mapOptionsToLevel,parentColor:b,parentColorIndex:d,series:n,siblings:f});if(b=n.points[a.i])b.color=
k.color,b.colorIndex=k.colorIndex;(a.children||[]).forEach(function(b,d){n.setColorRecursive(b,k.color,k.colorIndex,d,a.children.length)})}},algorithmGroup:function(a,b,d,c){this.height=a;this.width=b;this.plot=c;this.startDirection=this.direction=d;this.lH=this.nH=this.lW=this.nW=this.total=0;this.elArr=[];this.lP={total:0,lH:0,nH:0,lW:0,nW:0,nR:0,lR:0,aspectRatio:function(a,b){return Math.max(a/b,b/a)}};this.addElement=function(a){this.lP.total=this.elArr[this.elArr.length-1];this.total+=a;0===
this.direction?(this.lW=this.nW,this.lP.lH=this.lP.total/this.lW,this.lP.lR=this.lP.aspectRatio(this.lW,this.lP.lH),this.nW=this.total/this.height,this.lP.nH=this.lP.total/this.nW,this.lP.nR=this.lP.aspectRatio(this.nW,this.lP.nH)):(this.lH=this.nH,this.lP.lW=this.lP.total/this.lH,this.lP.lR=this.lP.aspectRatio(this.lP.lW,this.lH),this.nH=this.total/this.width,this.lP.nW=this.lP.total/this.nH,this.lP.nR=this.lP.aspectRatio(this.lP.nW,this.nH));this.elArr.push(a)};this.reset=function(){this.lW=this.nW=
0;this.elArr=[];this.total=0}},algorithmCalcPoints:function(a,b,d,n){var f,e,t,k,l=d.lW,h=d.lH,g=d.plot,m,q=0,p=d.elArr.length-1;b?(l=d.nW,h=d.nH):m=d.elArr[d.elArr.length-1];d.elArr.forEach(function(a){if(b||q<p)0===d.direction?(f=g.x,e=g.y,t=l,k=a/t):(f=g.x,e=g.y,k=h,t=a/k),n.push({x:f,y:e,width:t,height:c.correctFloat(k)}),0===d.direction?g.y+=k:g.x+=t;q+=1});d.reset();0===d.direction?d.width-=l:d.height-=h;g.y=g.parent.y+(g.parent.height-d.height);g.x=g.parent.x+(g.parent.width-d.width);a&&(d.direction=
1-d.direction);b||d.addElement(m)},algorithmLowAspectRatio:function(a,b,d){var c=[],f=this,e,g={x:b.x,y:b.y,parent:b},k=0,l=d.length-1,h=new this.algorithmGroup(b.height,b.width,b.direction,g);d.forEach(function(d){e=d.val/b.val*b.height*b.width;h.addElement(e);h.lP.nR>h.lP.lR&&f.algorithmCalcPoints(a,!1,h,c,g);k===l&&f.algorithmCalcPoints(a,!0,h,c,g);k+=1});return c},algorithmFill:function(a,b,d){var c=[],f,e=b.direction,g=b.x,k=b.y,h=b.width,l=b.height,m,q,p,r;d.forEach(function(d){f=d.val/b.val*
b.height*b.width;m=g;q=k;0===e?(r=l,p=f/r,h-=p,g+=p):(p=h,r=f/p,l-=r,k+=r);c.push({x:m,y:q,width:p,height:r});a&&(e=1-e)});return c},strip:function(a,b){return this.algorithmLowAspectRatio(!1,a,b)},squarified:function(a,b){return this.algorithmLowAspectRatio(!0,a,b)},sliceAndDice:function(a,b){return this.algorithmFill(!0,a,b)},stripes:function(a,b){return this.algorithmFill(!1,a,b)},translate:function(){var a=this,b=a.options,d=K(a),c,f;z.prototype.translate.call(a);f=a.tree=a.getTree();c=a.nodeMap[d];
a.renderTraverseUpButton(d);a.mapOptionsToLevel=y({from:c.level+1,levels:b.levels,to:f.height,defaults:{levelIsConstant:a.options.levelIsConstant,colorByPoint:b.colorByPoint}});""===d||c&&c.children.length||(a.setRootNode("",!1),d=a.rootNode,c=a.nodeMap[d]);E(a.nodeMap[a.rootNode],function(b){var c=!1,d=b.parent;b.visible=!0;if(d||""===d)c=a.nodeMap[d];return c});E(a.nodeMap[a.rootNode].children,function(a){var b=!1;a.forEach(function(a){a.visible=!0;a.children.length&&(b=(b||[]).concat(a.children))});
return b});a.setTreeValues(f);a.axisRatio=a.xAxis.len/a.yAxis.len;a.nodeMap[""].pointValues=d={x:0,y:0,width:100,height:100};a.nodeMap[""].values=d=r(d,{width:d.width*a.axisRatio,direction:"vertical"===b.layoutStartingDirection?0:1,val:f.val});a.calculateChildrenAreas(f,d);a.colorAxis?a.translateColors():b.colorByPoint||a.setColorRecursive(a.tree);b.allowTraversingTree&&(b=c.pointValues,a.xAxis.setExtremes(b.x,b.x+b.width,!1),a.yAxis.setExtremes(b.y,b.y+b.height,!1),a.xAxis.setScale(),a.yAxis.setScale());
a.setPointValues()},drawDataLabels:function(){var a=this,b=a.mapOptionsToLevel,d,c;a.points.filter(function(a){return a.node.visible}).forEach(function(f){c=b[f.node.level];d={style:{}};f.node.isLeaf||(d.enabled=!1);c&&c.dataLabels&&(d=r(d,c.dataLabels),a._hasPointLabels=!0);f.shapeArgs&&(d.style.width=f.shapeArgs.width,f.dataLabel&&f.dataLabel.css({width:f.shapeArgs.width+"px"}));f.dlOptions=r(d,f.options.dataLabels)});z.prototype.drawDataLabels.call(this)},alignDataLabel:function(a,b,d){var e=d.style;
!c.defined(e.textOverflow)&&b.text&&b.getBBox().width>b.text.textWidth&&b.css({textOverflow:"ellipsis",width:e.width+="px"});l.column.prototype.alignDataLabel.apply(this,arguments);a.dataLabel&&a.dataLabel.attr({zIndex:(a.node.zIndex||0)+1})},pointAttribs:function(a,b){var c=H(this.mapOptionsToLevel)?this.mapOptionsToLevel:{},e=a&&c[a.node.level]||{},c=this.options,f=b&&c.states[b]||{},g=a&&a.getClassName()||"";a={stroke:a&&a.borderColor||e.borderColor||f.borderColor||c.borderColor,"stroke-width":q(a&&
a.borderWidth,e.borderWidth,f.borderWidth,c.borderWidth),dashstyle:a&&a.borderDashStyle||e.borderDashStyle||f.borderDashStyle||c.borderDashStyle,fill:a&&a.color||this.color};-1!==g.indexOf("highcharts-above-level")?(a.fill="none",a["stroke-width"]=0):-1!==g.indexOf("highcharts-internal-node-interactive")?(b=q(f.opacity,c.opacity),a.fill=x(a.fill).setOpacity(b).get(),a.cursor="pointer"):-1!==g.indexOf("highcharts-internal-node")?a.fill="none":b&&(a.fill=x(a.fill).brighten(f.brightness).get());return a},
drawPoints:function(){var a=this,b=a.points.filter(function(a){return a.node.visible});b.forEach(function(b){var c="level-group-"+b.node.levelDynamic;a[c]||(a[c]=a.chart.renderer.g(c).attr({zIndex:1E3-b.node.levelDynamic}).add(a.group));b.group=a[c]});l.column.prototype.drawPoints.call(this);this.colorAttribs&&a.chart.styledMode&&this.points.forEach(function(a){a.graphic&&a.graphic.css(this.colorAttribs(a))},this);a.options.allowTraversingTree&&b.forEach(function(b){b.graphic&&(b.drillId=a.options.interactByLeaf?
a.drillToByLeaf(b):a.drillToByGroup(b))})},onClickDrillToNode:function(a){var b=(a=a.point)&&a.drillId;h(b)&&(a.setState(""),this.setRootNode(b,!0,{trigger:"click"}))},drillToByGroup:function(a){var b=!1;1!==a.node.level-this.nodeMap[this.rootNode].level||a.node.isLeaf||(b=a.id);return b},drillToByLeaf:function(a){var b=!1;if(a.node.parent!==this.rootNode&&a.node.isLeaf)for(a=a.node;!b;)a=this.nodeMap[a.parent],a.parent===this.rootNode&&(b=a.id);return b},drillUp:function(){var a=this.nodeMap[this.rootNode];
a&&h(a.parent)&&this.setRootNode(a.parent,!0,{trigger:"traverseUpButton"})},drillToNode:function(a,b){p("WARNING: treemap.drillToNode has been renamed to treemap.setRootNode, and will be removed in the next major version.");this.setRootNode(a,b)},setRootNode:function(a,b,c){a=A({newRootId:a,previousRootId:this.rootNode,redraw:q(b,!0),series:this},c);g(this,"setRootNode",a,function(a){var b=a.series;b.idPreviousRoot=a.previousRootId;b.rootNode=a.newRootId;b.isDirty=!0;a.redraw&&b.chart.redraw()})},
renderTraverseUpButton:function(a){var b=this,c=b.options.traverseUpButton,e=q(c.text,b.nodeMap[a].name,"\x3c Back"),f;""===a?b.drillUpButton&&(b.drillUpButton=b.drillUpButton.destroy()):this.drillUpButton?(this.drillUpButton.placed=!1,this.drillUpButton.attr({text:e}).align()):(f=(a=c.theme)&&a.states,this.drillUpButton=this.chart.renderer.button(e,null,null,function(){b.drillUp()},a,f&&f.hover,f&&f.select).addClass("highcharts-drillup-button").attr({align:c.position.align,zIndex:7}).add().align(c.position,
!1,c.relativeTo||"plotBox"))},buildKDTree:u,drawLegendSymbol:c.LegendSymbolMixin.drawRectangle,getExtremes:function(){z.prototype.getExtremes.call(this,this.colorValueData);this.valueMin=this.dataMin;this.valueMax=this.dataMax;z.prototype.getExtremes.call(this)},getExtremesFromAll:!0,bindAxes:function(){var a={endOnTick:!1,gridLineWidth:0,lineWidth:0,min:0,dataMin:0,minPadding:0,max:100,dataMax:100,maxPadding:0,startOnTick:!1,title:null,tickPositions:[]};z.prototype.bindAxes.call(this);c.extend(this.yAxis.options,
a);c.extend(this.xAxis.options,a)},utils:{recursive:E}},{getClassName:function(){var a=c.Point.prototype.getClassName.call(this),b=this.series,d=b.options;this.node.level<=b.nodeMap[b.rootNode].level?a+=" highcharts-above-level":this.node.isLeaf||q(d.interactByLeaf,!d.allowTraversingTree)?this.node.isLeaf||(a+=" highcharts-internal-node"):a+=" highcharts-internal-node-interactive";return a},isValid:function(){return this.id||v(this.value)},setState:function(a){c.Point.prototype.setState.call(this,
a);this.graphic&&this.graphic.attr({zIndex:"hover"===a?1:0})},setVisible:l.pie.prototype.pointClass.prototype.setVisible})})(m,F)});
//# sourceMappingURL=treemap.js.map