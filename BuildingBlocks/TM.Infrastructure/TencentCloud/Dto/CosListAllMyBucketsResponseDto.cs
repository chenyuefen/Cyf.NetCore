using COSXML.Model.Tag;
using System.Collections.Generic;

namespace TM.Infrastructure.CosCloud.Dto
{
    public class CosListAllMyBucketsResponseDto : CosResultInfoResponseDto
    {
        /// <summary>
        /// 存储桶列表
        /// </summary>
        public List<ListAllMyBuckets.Bucket> BucketsList { get; set; }
    }
}
