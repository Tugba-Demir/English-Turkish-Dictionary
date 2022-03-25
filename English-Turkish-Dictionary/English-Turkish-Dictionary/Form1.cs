using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_Turkish_Dictionary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DictionaryDal _dictionaryDal = new DictionaryDal();

        private void Form1_Load(object sender, EventArgs e)
        {
            dgv();
        }

        #region btnAdd
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Dictionary word = new Dictionary
            {
                Id = txtIdAdd.Text.ToString(),
                English = txtEngWordAdd.Text.ToString(),
                Turkish = txtTurkWordAdd.Text.ToString()
            };
            _dictionaryDal.Add(word);
            MessageBox.Show("Added new word!","To Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Clear();
            dgv();
        }
        #endregion

        #region btnUpdate
        private void dgvDictionary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdUpdate.Text = dgvDictionary.CurrentRow.Cells[0].Value.ToString();
            txtEngWordUpdate.Text = dgvDictionary.CurrentRow.Cells[1].Value.ToString();
            txtTurkWordUpdate.Text = dgvDictionary.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Dictionary word = new Dictionary
            {
                Id = txtIdUpdate.Text.ToString(),
                English = txtEngWordUpdate.Text.ToString(),
                Turkish = txtTurkWordUpdate.Text.ToString()
            };
            _dictionaryDal.Update(word);
            MessageBox.Show("Updated the word!", "To Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Clear();
            dgv();
        }
        #endregion

        #region btnRemove
        private void btnRemove_Click(object sender, EventArgs e)
        {
            Dictionary word = new Dictionary
            {
                Id = dgvDictionary.CurrentRow.Cells[0].Value.ToString()
            };
            _dictionaryDal.Remove(word);
            MessageBox.Show("Deleted the word!", "To Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Clear();
            dgv();
        }
        #endregion

        #region dgv        
        public void dgv()
        {
            dgvDictionary.DataSource = _dictionaryDal.GetAll();
        }
        #endregion

        #region Clear
        public void Clear()
        {
            txtIdAdd.Clear();
            txtIdUpdate.Clear();
            txtEngWordAdd.Clear();
            txtEngWordUpdate.Clear();
            txtTurkWordAdd.Clear();
            txtTurkWordUpdate.Clear();
        }

        #endregion

        #region btnCheck
        private void btnCheck_Click(object sender, EventArgs e)
        {
            List<Dictionary> kelimeler = _dictionaryDal.EnglishWords();
            

            int i = 0;
            int sayac = 0;
            while(kelimeler[i].English != null)
            {
                if (txtCheck.Text == kelimeler[i].English)  // ikisi de farklı türde bu yüzden ikisini de string aldım
                {
                    string message = "There is already this word!\n" + kelimeler[i].English + ": " + kelimeler[i].Turkish;
                    MessageBox.Show(message, "To Inform", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                else
                {
                    sayac++;
                    if (sayac == kelimeler.Count)
                    {
                        MessageBox.Show("You can add this word.", "To Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }
                i++;
            }
        }
        #endregion
    }
}
