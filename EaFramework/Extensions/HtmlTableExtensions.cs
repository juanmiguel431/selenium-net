using OpenQA.Selenium;

namespace EaFramework.Extensions;

public static class HtmlTableExtensions
{
    public static List<TableDataCollection> ReadTable(this IWebElement table)
    {
        var tableCollection = new List<TableDataCollection>();
        
        var tableHeaders = table.FindElements(By.TagName("th"));
        var tableRows = table.FindElements(By.TagName("tr"));

        var rowIndex = 0;

        foreach (var row in tableRows)
        {
            var columnIndex = 0;
            
            var columnData = row.FindElements(By.TagName("td"));
            
            foreach (var column in columnData)
            {
                tableCollection.Add(new TableDataCollection
                {
                    ColumnIndex = columnIndex,
                    RowIndex = rowIndex,
                    ColumnName = tableHeaders[columnIndex].Text,
                    ColumnValue = column.Text,
                    ColumnSpecialValues = GetControl(column),
                });
                
                columnIndex++;
            }
            
            rowIndex++;
        }
        
        return tableCollection;
    }

    public static void PerformActionOnCell(this IWebElement element, int targetColumnIndex, string refColumnName, string refColumnValue, string controlToOperate = null)
    {
        var table = element.ReadTable();
        
        foreach (var rowIndex in GetRowIndex(table, refColumnName, refColumnValue))
        {
            var cell = table.Where(p =>
                p.ColumnIndex == targetColumnIndex
                && p.RowIndex == rowIndex)
            .Select(p => p.ColumnSpecialValues)
            .SingleOrDefault();

            if (controlToOperate != null && cell != null)
            {
                IWebElement? returnedControl = null;
                if (cell.ControlType == ControlType.Hyperlink)
                {
                    returnedControl = cell.ElementCollection?.Where(p =>
                            p.Text == controlToOperate)
                        .SingleOrDefault();
                }
                else if (cell.ControlType == ControlType.Input)
                {
                    returnedControl = cell.ElementCollection?.Where(p => 
                        p.GetAttribute("value") == controlToOperate)
                        .SingleOrDefault();
                }
                
                returnedControl?.Click();
            }
            else
            {
                cell?.ElementCollection?.First().Click();
            }
        }
    }

    private static IEnumerable<int> GetRowIndex(List<TableDataCollection> tableCollection, string columnName, string columnValue)
    {
        foreach (var table in tableCollection)
        {
            if (table.ColumnName == columnName && table.ColumnValue == columnValue)
            {
                yield return table.RowIndex;
            }
        }
    }

    private static ColumnSpecialValue? GetControl(IWebElement column)
    {
        if (column.FindElements(By.TagName("a")).Count > 0)
        {
            return new ColumnSpecialValue
            {
                ElementCollection = column.FindElements(By.TagName("a")),
                ControlType = ControlType.Hyperlink
            };
        }

        if (column.FindElements(By.TagName("input")).Count > 0)
        {
            return new ColumnSpecialValue
            {
                ElementCollection = column.FindElements(By.TagName("input")),
                ControlType = ControlType.Input
            };
        }

        return null;
    }
}

public class TableDataCollection
{
    public int ColumnIndex { get; set; }
    public int RowIndex { get; set; }
    public string? ColumnName { get; set; }
    public string? ColumnValue { get; set; }
    public ColumnSpecialValue? ColumnSpecialValues { get; set; }
}

public class ColumnSpecialValue
{
    public IEnumerable<IWebElement>? ElementCollection { get; set; }
    public ControlType? ControlType { get; set; }
}

public enum ControlType
{
    Hyperlink,
    Input,
    Option,
    Select,
}