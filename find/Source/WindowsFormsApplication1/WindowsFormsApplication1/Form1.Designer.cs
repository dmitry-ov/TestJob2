namespace WindowsFormsApplication1
{
    partial class SearchForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.editNewDoc = new System.Windows.Forms.TextBox();
            this.buttonNewDoc = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textSearchResult = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.editSearch = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.listdoc = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.editNewDoc);
            this.groupBox1.Controls.Add(this.buttonNewDoc);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Новый текстовый документ";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(266, 59);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(112, 34);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // editNewDoc
            // 
            this.editNewDoc.Location = new System.Drawing.Point(6, 19);
            this.editNewDoc.Multiline = true;
            this.editNewDoc.Name = "editNewDoc";
            this.editNewDoc.Size = new System.Drawing.Size(254, 147);
            this.editNewDoc.TabIndex = 1;
            this.editNewDoc.Text = "Мама мыла раму\r\nРама мыться не хотела";
            // 
            // buttonNewDoc
            // 
            this.buttonNewDoc.Location = new System.Drawing.Point(266, 19);
            this.buttonNewDoc.Name = "buttonNewDoc";
            this.buttonNewDoc.Size = new System.Drawing.Size(112, 34);
            this.buttonNewDoc.TabIndex = 2;
            this.buttonNewDoc.Text = "Сохранить в БД";
            this.buttonNewDoc.UseVisualStyleBackColor = true;
            this.buttonNewDoc.Click += new System.EventHandler(this.buttonNewDoc_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textSearchResult);
            this.groupBox2.Controls.Add(this.buttonSearch);
            this.groupBox2.Controls.Add(this.editSearch);
            this.groupBox2.Location = new System.Drawing.Point(415, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 421);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Поиск";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Результаты поиска :";
            // 
            // textSearchResult
            // 
            this.textSearchResult.Location = new System.Drawing.Point(9, 79);
            this.textSearchResult.Multiline = true;
            this.textSearchResult.Name = "textSearchResult";
            this.textSearchResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textSearchResult.Size = new System.Drawing.Size(343, 334);
            this.textSearchResult.TabIndex = 8;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(266, 22);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(86, 20);
            this.buttonSearch.TabIndex = 7;
            this.buttonSearch.Text = "Поиск";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // editSearch
            // 
            this.editSearch.Location = new System.Drawing.Point(6, 22);
            this.editSearch.Name = "editSearch";
            this.editSearch.Size = new System.Drawing.Size(248, 20);
            this.editSearch.TabIndex = 6;
            this.editSearch.Text = "Мама раму";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBox);
            this.groupBox3.Controls.Add(this.listdoc);
            this.groupBox3.Location = new System.Drawing.Point(12, 198);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(388, 235);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Просмотр документов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Содержимое документов :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Номера  документов :";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(145, 42);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(233, 185);
            this.textBox.TabIndex = 5;
            // 
            // listdoc
            // 
            this.listdoc.FormattingEnabled = true;
            this.listdoc.Location = new System.Drawing.Point(9, 41);
            this.listdoc.Name = "listdoc";
            this.listdoc.Size = new System.Drawing.Size(116, 186);
            this.listdoc.TabIndex = 4;
            this.listdoc.SelectedIndexChanged += new System.EventHandler(this.listdoc_SelectedIndexChanged);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 443);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SearchForm";
            this.Padding = new System.Windows.Forms.Padding(800, 0, 0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиск";
            this.Shown += new System.EventHandler(this.SearchForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TextBox editNewDoc;
        private System.Windows.Forms.Button buttonNewDoc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSearchResult;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox editSearch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ListBox listdoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

