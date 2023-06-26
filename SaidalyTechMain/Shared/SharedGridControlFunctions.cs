using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaidalyTechMain.Shared
{
    internal class SharedGridControlFunctions
    {
        public void SetColumnEditsARepoMemo(GridView gridView, RepositoryItemMemoEdit repoMemoWithProperties, int[] columnsToEdit)
        {
            foreach (int columnIndex in columnsToEdit)
            {
                gridView.Columns[columnIndex].ColumnEdit = repoMemoWithProperties;
            }
        }
        public void HideGridColumns(GridView gridView, int[] columnsToHide)
        {
            foreach (int columnIndex in columnsToHide)
            {
                gridView.Columns[columnIndex].Visible = false;
            }
        }
        public void ClearGridView(GridView gridView)
        {
            while (gridView.RowCount != 0)
            {
                gridView.DeleteRow(0);
            }
        }
    }
}
