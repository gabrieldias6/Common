using System.Collections;

namespace GD6.Common
{
  public class DatatablesRetorno
  {
    public int draw { get; set; }
    public int recordsTotal { get; set; }
    public int recordsFiltered { get; set; }
    public IEnumerable data { get; set; }
  }
}
