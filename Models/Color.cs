namespace GlassECommerce.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        //
        public virtual ICollection<Model> Models { get; set; }
    }
}
