﻿using System;
using Domain.Common.Enums;
using Hx.Domain.Entities;

namespace Domain.Common.Dtos.Order
{
    /// <summary>
    /// 订单
    /// </summary>
    public class OrderDto
    {
        public string Id { get; set; }
        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public ProductType ProductType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public OrderType OrderType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    public class OrderPara : PageBase
    {
        public string Id { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public OrderType OrderType { get; set; }
   
       
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
