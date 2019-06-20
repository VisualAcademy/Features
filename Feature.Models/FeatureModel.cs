using System;

namespace Feature.Models
{
    /// <summary>
    /// Features 테이블과 일대일로 매핑되는 모델 클래스
    /// </summary>
    public class FeatureModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}
