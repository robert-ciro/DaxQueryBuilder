using DaxQueryBuilder.Enums;
using DaxQueryBuilder.DaxFunctions.Measures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaxQueryBuilder
{
    public class DaxQueryBuilder
    {
        private List<string> _selectBuilder;
        private List<string> _filterBuilder;
        private List<string> _measureFilterBuilder;
        private List<string> _measureBuilder;
        private List<string> _orderByBuilder;
        private DaxFilter _filter;
        private DaxMeasureFilter _measureFilter;

        public DaxQueryBuilder()
        {
            _selectBuilder = new List<string>();
            _filterBuilder = new List<string>();
            _measureFilterBuilder = new List<string>();
            _measureBuilder = new List<string>();
            _orderByBuilder = new List<string>();
            _filter = new DaxFilter();
            _measureFilter = new DaxMeasureFilter();
        }
        public DaxQueryBuilder SelectColumn(string table, string column)
        {
            _selectBuilder.Add($"{table}[{column}]");
            return this;
        }

        public DaxQueryBuilder SelectMeasure(string measure)
        {
            _measureBuilder.Add($@"""{measure}"", [{measure}]"); 
            return this;
        }

        public DaxQueryBuilder ApplyFilter(Func<DaxFilter, string> action)
        {
            if (action != null)
            {
                _filterBuilder.Add(action.Invoke(_filter));
            }

            return this;
        }

        public DaxQueryBuilder ApplyMeasureFilter(Func<DaxMeasureFilter, string> action)
        {
            if (action != null)
            {
                _filterBuilder.Add(action.Invoke(_measureFilter));
            }

            return this;
        }

        public DaxQueryBuilder OrderBy(string column, Orders order = Orders.ASC)
        {
            _orderByBuilder.Add($"[{column}] {order}");
            return this;
        }

        public DaxQueryBuilder OrderBy(string table, string column, Orders order = Orders.ASC)
        {
            _orderByBuilder.Add($"{table}[{column}] {order}");
            return this;
        }
        public string GetQuery()
        {
            var s = new StringBuilder();
            s.Append("EVALUATE SUMMARIZECOLUMNS(");
            s.Append(ConcatanteInSummarizeColumns(_selectBuilder, _filterBuilder, _measureFilterBuilder, _measureBuilder));
            s.Append(")");

            if (_orderByBuilder.Any())
            {
                s.Append("ORDER BY ");
                s.Append(string.Join(",", _orderByBuilder));
            }

            return s.ToString();
        }

        private string ConcatanteInSummarizeColumns(params List<string>[] group)
        {
            return string.Join(",", group.Where(list => list.Count > 0).Select(list => string.Join(",", list)));
        }
    }
}
