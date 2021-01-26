using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Infrastructure.Tencent.Module
{
    public class MapResponse<T>
    {
        /// <summary>
        /// 状态码，0为正常,
        /// 310请求参数信息有误，
        /// 311Key格式错误,
        /// 306请求有护持信息请检查字符串,
        /// 110请求来源未被授权
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 对status的描述
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 计算结果
        /// </summary>
        public T result { get; set; }
    }

    //如果好用，请收藏地址，帮忙分享。
    public class ElementsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int distance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int duration { get; set; }
    }

    public class RowsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ElementsItem> elements { get; set; }
    }

    public class DistanceResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<RowsItem> rows { get; set; }
    }

    public class Location
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double lat { get; set; }
    }

    public class Ad_info
    {
        /// <summary>
        /// 
        /// </summary>
        public string adcode { get; set; }
    }

    public class Address_components
    {
        /// <summary>
        /// 北京市
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 北京市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 海淀区
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 海淀西大街
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string street_number { get; set; }
    }

    public class GeocoderResult
    {
        /// <summary>
        /// 海淀西大街74号
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Ad_info ad_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Address_components address_components { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double similarity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int deviation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int reliability { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 广东省广州市天河区天府路
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Formatted_addresses formatted_addresses { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Address_component address_component { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Address_reference address_reference { get; set; }
    }

    public class Formatted_addresses
    {
        /// <summary>
        /// 广州市天河区人民政府(天府路西)
        /// </summary>
        public string recommend { get; set; }
        /// <summary>
        /// 广州市天河区人民政府(天府路西)
        /// </summary>
        public string rough { get; set; }
    }

    public class Address_component
    {
        /// <summary>
        /// 中国
        /// </summary>
        public string nation { get; set; }
        /// <summary>
        /// 广东省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 广州市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 天河区
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 天府路
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// 天府路
        /// </summary>
        public string street_number { get; set; }
    }

    public class Street_number
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double _distance { get; set; }
        /// <summary>
        /// 西
        /// </summary>
        public string _dir_desc { get; set; }
    }

    public class Crossroad
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 黄埔大道中/员村二横路(路口)
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double _distance { get; set; }
        /// <summary>
        /// 北
        /// </summary>
        public string _dir_desc { get; set; }
    }

    public class Town
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 天园街道
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int _distance { get; set; }
        /// <summary>
        /// 内
        /// </summary>
        public string _dir_desc { get; set; }
    }

    public class Street
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 天府路
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double _distance { get; set; }
        /// <summary>
        /// 西
        /// </summary>
        public string _dir_desc { get; set; }
    }

    public class Landmark_l1
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 天河公园
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double _distance { get; set; }
        /// <summary>
        /// 西南
        /// </summary>
        public string _dir_desc { get; set; }
    }

    public class Landmark_l2
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 广州市天河区人民政府
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double _distance { get; set; }
        /// <summary>
        /// 内
        /// </summary>
        public string _dir_desc { get; set; }
    }

    public class Address_reference
    {
        /// <summary>
        /// 
        /// </summary>
        public Street_number street_number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Crossroad crossroad { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Town town { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Street street { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Landmark_l1 landmark_l1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Landmark_l2 landmark_l2 { get; set; }
    }
}
