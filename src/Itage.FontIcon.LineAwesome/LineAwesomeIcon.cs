namespace Itage.FontIcon.LineAwesome
{
    // CSS FIX
    // \.(.*):before\s*{\s*content:\s*"\\(.*)"; }
    // $1 = 0x$2,

    public class LineAwesomeIcon : BaseFontIcon<LineAwesomeIconType>
    {
        static LineAwesomeIcon()
        {
            DefaultFontFamily = FontResourceHelper.MakeFontFamily(typeof(LineAwesomeIcon).Assembly, "resources", "LineAwesome");
        }
        public LineAwesomeIcon()
        {
            FontFamily = DefaultFontFamily;
        }
    }
}
