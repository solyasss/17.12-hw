using System.Windows.Media;

namespace hw.Models
{
    public class color_model
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public Color ToColor()
        {
            return Color.FromArgb(A, R, G, B);
        }

        public SolidColorBrush Color_brush
        {
            get
            {
                return new SolidColorBrush(ToColor());
            }
        }

        public string Color_display
        {
            get
            {
                return $"A:{A}, R:{R}, G:{G}, B:{B}";
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is color_model other)
            {
                return A == other.A && R == other.R && G == other.G && B == other.B;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return A.GetHashCode() ^ R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode();
        }
    }
}