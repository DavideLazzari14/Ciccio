using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        // Lista di ricette (ogni voce è una stringa contenente titolo, ingredienti e preparazione)
        private List<string> recipes;
        // Dispensa: insieme di ingredienti disponibili (case-insensitive)
        private HashSet<string> pantry;
        // Lista della spesa calcolata in base alla ricetta selezionata
        private List<string> shoppingList;
        // Indice dell'ultimo ordine preso (usato per generare HTML coerente)
        private int lastOrderedIndex = -1;

        public Form1()
        {
            // Costruttore: inizializza componenti e collezioni interne
            InitializeComponent();
            recipes = new List<string>();
            pantry = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            shoppingList = new List<string>();
        }

        // Evento collegato nel Designer: OnForm1Load
        private void OnForm1Load(object sender, EventArgs e)
        {
            // All'avvio carica dati di esempio, popola il menù, la dispensa e la combo dei prodotti
            LoadSampleData();
            PopulateMenu();
            RefreshPantryList();
            PopulateProductCombo();
            // Assicura che la combo contenga tutti gli ingredienti usati nelle ricette
            EnsureProductsIncludeAllIngredients();
        }

        // Aggiunge alla combo dei prodotti gli ingredienti trovati nelle ricette se mancanti
        private void EnsureProductsIncludeAllIngredients()
        {
            if (cmbProducts == null || recipes == null) return;
            var all = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var r in recipes)
            {
                foreach (var ing in ParseIngredients(r)) all.Add(ing);
            }
            foreach (var ing in all)
            {
                if (!cmbProducts.Items.Contains(ing)) cmbProducts.Items.Add(ing);
            }
            if (cmbProducts.Items.Count > 0 && cmbProducts.SelectedIndex < 0) cmbProducts.SelectedIndex = 0;
        }

        // Campi per immagini (mantenuti vuoti/di riserva). Non usati nella versione semplificata
        private Dictionary<string, string> productImage = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, string> recipeImage = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static readonly System.Net.Http.HttpClient _httpClient = new System.Net.Http.HttpClient();

        private void PopulateProductCombo()
        {
            if (cmbProducts == null) return;
            // Popola la ComboBox con una lista di prodotti campione
            var products = new[] { "zucchero", "uova", "savoiardi", "mascarpone", "caffè", "cioccolato", "farina", "lievito", "panna", "burro", "marmellata", "ricotta", "fragole", "vaniglia" };
            cmbProducts.Items.Clear();
            cmbProducts.Items.AddRange(products.Cast<object>().ToArray());
            if (cmbProducts.Items.Count > 0) cmbProducts.SelectedIndex = 0;
            // Nota: non vengono caricate immagini nella versione semplificata
        }

        // (Rimosse funzioni di gestione immagini per semplicità)

        private void LoadSampleData()
        {
            // Carica dati di esempio: ricette e dispensa iniziale
            recipes = new List<string> {
                "Torta Al Cioccolato e Panna. Ingredienti: cioccolato, farina, zucchero, uova, panna. Preparazione: sciogliere il cioccolato, mescolare, infornare.",
                "Tiramisù. Ingredienti: zucchero, uova, savoiardi, mascarpone, caffè. Preparazione: unire le uova con lo zucchero, aggiungere il mascarpone, etc.",
                "Bavarese alle Fragole. Ingredienti: fragole, panna, zucchero, gelatina. Preparazione: frullare le fragole, unire la panna montata.",
                "Crostata alla Marmellata. Ingredienti: farina, zucchero, burro, uova, marmellata. Preparazione: impastare, stendere, farcire e cuocere.",
                "Sfogliatine. Ingredienti: pasta sfoglia, zucchero, crema pasticcera. Preparazione: tagliare, farcire e cuocere.",
                "Cannoli. Ingredienti: ricotta, zucchero, scorza di arancia, cialde. Preparazione: preparare la crema e riempire le cialde.",
                "Panna Cotta. Ingredienti: panna, zucchero, gelatina, vaniglia. Preparazione: scaldare la panna con zucchero, aggiungere gelatina.",
                "Zuppa Inglese. Ingredienti: crema, alchermes, savoiardi, cioccolato. Preparazione: stratificare i savoiardi e la crema.",
                "Millefoglie. Ingredienti: pasta sfoglia, crema pasticcera, zucchero a velo. Preparazione: stratificare e spolverare.",
                "Semifreddo al Limone. Ingredienti: limoni, zucchero, panna, uova. Preparazione: montare gli ingredienti e congelare."
            };

            // Dispensa prepopolata
            pantry = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "uova", "cioccolato", "mascarpone", "farina", "lievito", "zucchero", "burro" };
        }

        private void PopulateMenu()
        {
            // Mostra l'elenco delle ricette nel ListBox del menù
            lstMenu.Items.Clear();
            if (recipes != null && recipes.Count > 0)
                lstMenu.Items.AddRange(recipes.Select((r, i) => $"{i + 1}) {GetRecipeName(r)}").Cast<object>().ToArray());
        }

        private string GetRecipeName(string recipe)
        {
            // Estrae il titolo della ricetta fino al primo punto
            int dot = recipe.IndexOf('.');
            if (dot > 0)
                return recipe.Substring(0, dot).Trim();
            return recipe;
        }

        private List<string> ParseIngredients(string recipe)
        {
            // Estrae gli ingredienti dalla stringa della ricetta
            const string marker = "Ingredienti:";
            int idx = recipe?.IndexOf(marker, StringComparison.OrdinalIgnoreCase) ?? -1;
            if (idx < 0) return new List<string>();
            int start = idx + marker.Length;
            int prepIdx = recipe.IndexOf("Preparazione:", start, StringComparison.OrdinalIgnoreCase);
            string ingPart = prepIdx >= 0 ? recipe.Substring(start, prepIdx - start) : recipe.Substring(start);
            // Divide per virgole o punto e virgola, pulisce spazi e punti finali
            return ingPart.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(p => p.Trim().TrimEnd('.'))
                          .Where(s => !string.IsNullOrEmpty(s))
                          .ToList();
        }

        // Evento collegato nel Designer: lstMenu_SelectedIndexChanged
        private async void lstMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMenu.SelectedIndex < 0) return;
            string recipe = recipes[lstMenu.SelectedIndex];
            var ingredients = ParseIngredients(recipe);
            lstIngredients.Items.Clear();
            foreach (var ing in ingredients) lstIngredients.Items.Add(ing);
            // Mostra il testo completo della ricetta nel riquadro dei dettagli
            txtRecipeDetails.Text = recipe;
        }

        // Nessuna logica di caricamento immagini nella versione semplificata

        // Evento collegato nel Designer: OnBtnOrderClick
        private void OnBtnOrderClick(object sender, EventArgs e)
        {
            if (lstMenu.SelectedIndex < 0)
            {
                MessageBox.Show("Seleziona un dolce dal menù.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = lstMenu.SelectedIndex;
            string recipe = recipes[index];
            var ingredients = ParseIngredients(recipe);

            // Costruisce la lista degli ingredienti mancanti (senza duplicati, case-insensitive)
            var missing = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var ing in ingredients)
            {
                if (!pantry.Contains(ing)) missing.Add(ing);
            }
            shoppingList = missing.OrderBy(s => s).ToList();

            // Salva l'indice dell'ultimo ordine e aggiorna le liste dell'interfaccia utente
            lastOrderedIndex = index;
            lstShopping.Items.Clear();
            foreach (var s in shoppingList) lstShopping.Items.Add(s);

            // Aggiorna la visualizzazione della dispensa
            RefreshPantryList();

            var sb = new StringBuilder();
            sb.AppendLine($"Dolce scelto: {GetRecipeName(recipe)}");
            sb.AppendLine("Ingredienti richiesti: " + string.Join(", ", ingredients));
            sb.AppendLine("Dispensa: " + string.Join(", ", pantry.OrderBy(x => x)));
            sb.AppendLine("Lista della spesa (mancanti): " + (shoppingList.Count == 0 ? "(nessuno)" : string.Join(", ", shoppingList)));
            // Mostra riepilogo nell'area di output
            txtOutput.Text = sb.ToString();
        }

        // Evento collegato nel Designer: OnBtnGenerateHtmlClick
        private void OnBtnGenerateHtmlClick(object sender, EventArgs e)
        {
            string html = GenerateHtmlReport();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "lista_spesa.html");
            File.WriteAllText(path, html, Encoding.UTF8);
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = path, UseShellExecute = true });
            }
            catch { }
            MessageBox.Show($"File HTML generato: {path}", "Fatto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GenerateHtmlReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"it\">");
            sb.AppendLine("<head><meta charset=\"utf-8\"><title>Lista della Spesa</title>");
            sb.AppendLine("<style>body{font-family:Segoe UI,Arial;margin:20px}h1,h2{color:#333}ul{line-height:1.6}</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            // Mostra la ricetta dell'ultimo ordine preso; se non esiste usa la selezione attuale
            int showIndex = lastOrderedIndex >= 0 ? lastOrderedIndex : (lstMenu.SelectedIndex >= 0 ? lstMenu.SelectedIndex : -1);
            if (showIndex >= 0)
            {
                var chosen = GetRecipeName(recipes[showIndex]);
                sb.AppendLine($"<h2>Dolce scelto: {WebUtility.HtmlEncode(chosen)}</h2>");
            }
            sb.AppendLine("<h1>Lista della Spesa</h1>");
            if (shoppingList == null || shoppingList.Count == 0)
            {
                sb.AppendLine("<p>Nessun elemento da comprare.</p>");
            }
            else
            {
                sb.AppendLine("<ul>");
                foreach (var s in shoppingList)
                {
                    sb.AppendLine($"<li>{WebUtility.HtmlEncode(s)}</li>");
                }
                sb.AppendLine("</ul>");
            }
            // Sezione dispensa nell'HTML
            sb.AppendLine("<h2>Dispensa</h2>");
            sb.AppendLine("<p>" + WebUtility.HtmlEncode(string.Join(", ", pantry.OrderBy(x => x))) + "</p>");
            // Aggiunge le istruzioni di preparazione per la ricetta selezionata
            if (showIndex >= 0)
            {
                var recipe = recipes[showIndex];
                var steps = ParsePreparationSteps(recipe);
                if (steps != null && steps.Count > 0)
                {
                    sb.AppendLine("<h2>Preparazione</h2>");
                    sb.AppendLine("<ol>");
                    foreach (var step in steps)
                    {
                        sb.AppendLine($"<li>{WebUtility.HtmlEncode(step)}</li>");
                    }
                    sb.AppendLine("</ol>");
                }
            }
            sb.AppendLine("</body></html>");
            return sb.ToString();
        }

        private List<string> ParsePreparationSteps(string recipe)
        {
            // Estrae e separa le fasi di preparazione
            const string marker = "Preparazione:";
            int idx = recipe?.IndexOf(marker, StringComparison.OrdinalIgnoreCase) ?? -1;
            if (idx < 0) return new List<string>();
            string part = recipe.Substring(idx + marker.Length).Trim();
            return part.Split(new[] { '.', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(p => p.Trim())
                       .Where(s => !string.IsNullOrEmpty(s))
                       .ToList();
        }

        // Aggiorna la visualizzazione della dispensa (UI)
        private void RefreshPantryList()
        {
            if (lstPantry == null) return;
            // Aggiorna ListBox della dispensa ordinando gli elementi
            lstPantry.Items.Clear();
            foreach (var p in pantry.OrderBy(s => s)) lstPantry.Items.Add(p);
        }

        // Evento collegato nel Designer: OnBtnAddPantryClick
        private void OnBtnAddPantryClick(object sender, EventArgs e)
        {
            // Aggiunge il prodotto selezionato nella Combo alla dispensa
            var v = cmbProducts?.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(v)) return;
            pantry.Add(v);
            RefreshPantryList();
        }

        // Evento collegato nel Designer: OnBtnRemovePantryClick
        private void OnBtnRemovePantryClick(object sender, EventArgs e)
        {
            // Rimuove l'elemento selezionato dalla dispensa
            if (lstPantry == null || lstPantry.SelectedItem == null) return;
            string v = lstPantry.SelectedItem.ToString();
            pantry.Remove(v);
            RefreshPantryList();
        }

        private void lstPantry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
