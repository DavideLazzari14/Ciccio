namespace WinFormsApp3
{
    partial class Form1
    {
        /// <summary>
        ///  Variabile richiesta dal designer.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Libera le risorse in uso.
        /// </summary>
        /// <param name="disposing">true se le risorse gestite devono essere eliminate; altrimenti false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.ListBox lstMenu;
        private System.Windows.Forms.ListBox lstIngredients;
        private System.Windows.Forms.ListBox lstPantry;
        private System.Windows.Forms.ListBox lstShopping;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Button btnGenerateHtml;
        private System.Windows.Forms.Button btnAddPantry;
        private System.Windows.Forms.Button btnRemovePantry;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtRecipeDetails;
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.PictureBox pictureBoxRecipe;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Label lblIngredients;
        private System.Windows.Forms.Label lblPantry;
        private System.Windows.Forms.Label lblShopping;

        #region Codice generato dal Windows Form Designer

        /// <summary>
        ///  Metodo richiesto per il supporto del Designer - non modificare
        ///  il contenuto di questo metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            lstMenu = new ListBox();
            lstIngredients = new ListBox();
            lstPantry = new ListBox();
            lstShopping = new ListBox();
            btnOrder = new Button();
            btnGenerateHtml = new Button();
            btnAddPantry = new Button();
            btnRemovePantry = new Button();
            txtOutput = new TextBox();
            txtRecipeDetails = new TextBox();
            cmbProducts = new ComboBox();
            pictureBoxRecipe = new PictureBox();
            lblMenu = new Label();
            lblIngredients = new Label();
            lblPantry = new Label();
            lblShopping = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRecipe).BeginInit();
            SuspendLayout();
            // 
            // lstMenu (Menù)
            // 
            lstMenu.FormattingEnabled = true;
            lstMenu.ItemHeight = 15;
            lstMenu.Location = new Point(12, 30);
            lstMenu.Name = "lstMenu";
            lstMenu.Size = new Size(280, 304);
            lstMenu.TabIndex = 0;
            lstMenu.SelectedIndexChanged += lstMenu_SelectedIndexChanged;
            // 
            // lstIngredients (Ingredienti)
            // 
            lstIngredients.FormattingEnabled = true;
            lstIngredients.ItemHeight = 15;
            lstIngredients.Location = new Point(306, 138);
            lstIngredients.Name = "lstIngredients";
            lstIngredients.Size = new Size(195, 184);
            lstIngredients.TabIndex = 3;
            // 
            // lstPantry (Dispensa)
            // 
            lstPantry.FormattingEnabled = true;
            lstPantry.ItemHeight = 15;
            lstPantry.Location = new Point(520, 32);
            lstPantry.Name = "lstPantry";
            lstPantry.Size = new Size(140, 199);
            lstPantry.TabIndex = 5;
            lstPantry.SelectedIndexChanged += lstPantry_SelectedIndexChanged;
            // 
            // lstShopping (Lista della spesa)
            // 
            lstShopping.FormattingEnabled = true;
            lstShopping.ItemHeight = 15;
            lstShopping.Location = new Point(12, 360);
            lstShopping.Name = "lstShopping";
            lstShopping.Size = new Size(280, 64);
            lstShopping.TabIndex = 9;
            // 
            // btnOrder (Prendi Ordine)
            // 
            btnOrder.Location = new Point(306, 32);
            btnOrder.Name = "btnOrder";
            btnOrder.Size = new Size(120, 30);
            btnOrder.TabIndex = 1;
            btnOrder.Text = "Prendi Ordine";
            btnOrder.UseVisualStyleBackColor = true;
            btnOrder.Click += OnBtnOrderClick;
            // 
            // btnGenerateHtml (Genera HTML)
            // 
            btnGenerateHtml.Location = new Point(306, 78);
            btnGenerateHtml.Name = "btnGenerateHtml";
            btnGenerateHtml.Size = new Size(120, 30);
            btnGenerateHtml.TabIndex = 2;
            btnGenerateHtml.Text = "Genera HTML";
            btnGenerateHtml.UseVisualStyleBackColor = true;
            btnGenerateHtml.Click += OnBtnGenerateHtmlClick;
            // 
            // btnAddPantry (+ dispensa)
            // 
            btnAddPantry.Location = new Point(520, 239);
            btnAddPantry.Name = "btnAddPantry";
            btnAddPantry.Size = new Size(34, 25);
            btnAddPantry.TabIndex = 7;
            btnAddPantry.Text = "+";
            btnAddPantry.UseVisualStyleBackColor = true;
            btnAddPantry.Click += OnBtnAddPantryClick;
            // 
            // btnRemovePantry (- dispensa)
            // 
            btnRemovePantry.Location = new Point(560, 239);
            btnRemovePantry.Name = "btnRemovePantry";
            btnRemovePantry.Size = new Size(34, 25);
            btnRemovePantry.TabIndex = 8;
            btnRemovePantry.Text = "-";
            btnRemovePantry.UseVisualStyleBackColor = true;
            btnRemovePantry.Click += OnBtnRemovePantryClick;
            // 
            // txtOutput (Output testi)
            // 
            txtOutput.Location = new Point(12, 440);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(776, 100);
            txtOutput.TabIndex = 10;
            // 
            // txtRecipeDetails (Dettagli ricetta)
            // 
            txtRecipeDetails.Location = new Point(306, 340);
            txtRecipeDetails.Multiline = true;
            txtRecipeDetails.Name = "txtRecipeDetails";
            txtRecipeDetails.ReadOnly = true;
            txtRecipeDetails.ScrollBars = ScrollBars.Vertical;
            txtRecipeDetails.Size = new Size(482, 93);
            txtRecipeDetails.TabIndex = 4;
            // 
            // cmbProducts (Elenco prodotti)
            // 
            cmbProducts.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducts.FormattingEnabled = true;
            cmbProducts.Location = new Point(520, 270);
            cmbProducts.Name = "cmbProducts";
            cmbProducts.Size = new Size(180, 23);
            cmbProducts.TabIndex = 11;
            // 
            // pictureBoxRecipe (Immagine ricetta)
            // 
            pictureBoxRecipe.Location = new Point(670, 32);
            pictureBoxRecipe.Name = "pictureBoxRecipe";
            pictureBoxRecipe.Size = new Size(120, 120);
            pictureBoxRecipe.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxRecipe.TabIndex = 12;
            pictureBoxRecipe.TabStop = false;
            // 
            // lblMenu (Etichetta Menù)
            // 
            lblMenu.AutoSize = true;
            lblMenu.Location = new Point(12, 12);
            lblMenu.Name = "lblMenu";
            lblMenu.Size = new Size(38, 15);
            lblMenu.TabIndex = 15;
            lblMenu.Text = "Menù";
            // 
            // lblIngredients (Etichetta Ingredienti)
            // 
            lblIngredients.AutoSize = true;
            lblIngredients.Location = new Point(306, 120);
            lblIngredients.Name = "lblIngredients";
            lblIngredients.Size = new Size(64, 15);
            lblIngredients.TabIndex = 14;
            lblIngredients.Text = "Ingredienti";
            // 
            // lblPantry (Etichetta Dispensa)
            // 
            lblPantry.AutoSize = true;
            lblPantry.Location = new Point(520, 12);
            lblPantry.Name = "lblPantry";
            lblPantry.Size = new Size(54, 15);
            lblPantry.TabIndex = 13;
            lblPantry.Text = "Dispensa";
            // 
            // lblShopping (Etichetta Lista della spesa)
            // 
            lblShopping.AutoSize = true;
            lblShopping.Location = new Point(12, 340);
            lblShopping.Name = "lblShopping";
            lblShopping.Size = new Size(91, 15);
            lblShopping.TabIndex = 16;
            lblShopping.Text = "Lista della spesa";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Controls.Add(btnRemovePantry);
            Controls.Add(btnAddPantry);
            Controls.Add(cmbProducts);
            Controls.Add(pictureBoxRecipe);
            Controls.Add(lstPantry);
            Controls.Add(lblPantry);
            Controls.Add(txtRecipeDetails);
            Controls.Add(lstIngredients);
            Controls.Add(lblIngredients);
            Controls.Add(lblMenu);
            Controls.Add(lstShopping);
            Controls.Add(lblShopping);
            Controls.Add(txtOutput);
            Controls.Add(btnGenerateHtml);
            Controls.Add(btnOrder);
            Controls.Add(lstMenu);
            Name = "Form1";
            Text = "Pasticceria - Ciccio e Renata";
            Load += OnForm1Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxRecipe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
