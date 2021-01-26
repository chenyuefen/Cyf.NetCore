namespace TM.Core.Models
{
    public interface IEntityDto
    {

    }
    //
    // 摘要:
    //     Defines common properties for entity based DTOs.
    //
    // 类型参数:
    //   TPrimaryKey:
    public interface IEntityDto<TKey> : IEntityDto
    {
        //
        // 摘要:
        //     Id of the entity.
        TKey Id { get; set; }
    }
}
