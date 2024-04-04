using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static StoreManagementWFA.StoreManagement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StoreManagementWFA
{
    public partial class StoreManagement : Form
    {

        //Default System


        public StoreManagement()
        {
            InitializeComponent();
        }

        private void tabControlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPageAddCategory_Click(object sender, EventArgs e)
        {

        }
        //Variables Declaration

        public struct Date
        {
            public int day, month, year;
        }
        public struct Product
        {
            public string productName, category, company, code;
            public Date expiryDate;
        }

        Product[] productArray = new Product[] { };
        string[] categoryArrayFromProduct = new string[] { };
        string[] categoryArrayNotFromProduct = new string[] { "Tablet", "Laptop 18inch", "Printer" };
        string[] categoryArray = new string[] { };

        //Event Handler

        private void StoreManagement_Load(object sender, EventArgs e)
        {
            productArray = dummyDataForProduct(productArray);
            displayAllProductOnList(productArray);
            syncCategoryArrayWithProductArray();
            displayAllCategoryOnList(categoryArray);
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            Product newProduct = addProduct();
            bool productValidated = validateNewProduct(newProduct);
            if (productValidated)
            {
                productArray = addNewProductToArray(productArray, newProduct);
                displayAllProductOnList(productArray);
                syncCategoryArrayWithProductArray();
                displayAllCategoryOnList(categoryArray);
                clearValueOfAddProductField();
            }
            else
            {
                displayAllProductOnList(productArray);
            }
        }

        private void buttonSearchProduct_Click(object sender, EventArgs e)
        {
            searchProductContainsProductName();
        }

        private void buttonlabelSerachEditDeleteProduct_Click(object sender, EventArgs e)
        {
            searchProducEqualProduct();
        }

        private void buttonDeleteProduct_Click(object sender, EventArgs e)
        {
            deleteProduct();

        }

        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            editProduct();
        }

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            addCategory();
        }

        private void buttonSearchCategory_Click(object sender, EventArgs e)
        {
            searchCategoryContainsCategory();
        }

        private void buttonSearchEditDeleteCategory_Click(object sender, EventArgs e)
        {
            searchCategoryEqualCategory();
        }

        private void buttonEditCategory_Click(object sender, EventArgs e)
        {
            editCategory();
        }

        private void buttonDeleteCategory_Click(object sender, EventArgs e)
        {
            deleteCategory();
        }
        //Functions


        public void displayAllProductOnList(Product[] newProductArray)
        {
            listViewListProduct.Items.Clear();
            for (int i = 0; i < newProductArray.Length; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = newProductArray[i].productName;
                item.SubItems.Add(newProductArray[i].code);
                item.SubItems.Add(newProductArray[i].category);
                item.SubItems.Add(newProductArray[i].expiryDate.day.ToString()
                                    + "/" + newProductArray[i].expiryDate.month.ToString()
                                    + "/" + newProductArray[i].expiryDate.year.ToString());
                item.SubItems.Add(newProductArray[i].company);
                listViewListProduct.Items.Add(item);
            }
        }

        public Product addProduct()
        {
            Product newProduct;
            newProduct.productName = textBoxAddProductName.Text.Trim();
            newProduct.category = textBoxAddCategoryProduct.Text.Trim();
            newProduct.code = textBoxAddCodeProduct.Text.Trim();
            newProduct.expiryDate.day = ((int)numericUpDownDay.Value);
            newProduct.expiryDate.month = ((int)numericUpDownMonth.Value);
            newProduct.expiryDate.year = ((int)numericUpDownYear.Value);
            newProduct.company = textBoxAddProductCompany.Text.Trim();
            return newProduct;
        }

        public Product[] addNewProductToArray(Product[] currentProductArray, Product newProduct)
        {
            Product[] newProductArr = new Product[currentProductArray.Length + 1];
            for (int i = 0; i < currentProductArray.Length; i++)
            {
                newProductArr[i] = currentProductArray[i];
            }
            newProductArr[currentProductArray.Length] = newProduct;

            return newProductArr;
        }

        public Product[] dummyDataForProduct(Product[] currentProductArray)
        {
            Product[] newProductArr = new Product[4];
            newProductArr[0] = new Product { productName = "Iphone 15", category = "Phone", code = "1001", expiryDate = { day = 03, month = 11, year = 2023 }, company = "FPT" };
            newProductArr[1] = new Product { productName = "Cardinal", category = "Smart Watch", code = "2001", expiryDate = { day = 25, month = 02, year = 2023 }, company = "Fossil" };
            newProductArr[2] = new Product { productName = "ASUS VivoBook", category = "Laptop 12inch", code = "3001", expiryDate = { day = 12, month = 05, year = 2023 }, company = "ASUS" };
            newProductArr[3] = new Product { productName = "Samsung A12", category = "Phone", code = "4001", expiryDate = { day = 12, month = 05, year = 2023 }, company = "ASUS" };
            currentProductArray = newProductArr;
            return currentProductArray;
        }

        public bool validateNewProduct(Product newProduct)
        {
            if (newProduct.productName.Trim() == "")
            {
                labelAddProductMessage.Text = "Please fill in field: Product Name!";
                labelAddProductMessage.ForeColor = Color.Yellow;
                return false;
            };

            Product[] resProductName = Array.FindAll(productArray, ele => ele.productName.Equals(newProduct.productName));

            if (resProductName.Length > 0)
            {
                labelAddProductMessage.Text = "Product Name is Duplicated!";
                labelAddProductMessage.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.category.Trim() == "")
            {
                labelAddProductMessage.Text = "Please fill in field: Category";
                labelAddProductMessage.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.code.Trim() == "")
            {
                labelAddProductMessage.Text = "Please fill in field: Code!";
                labelAddProductMessage.ForeColor = Color.Yellow;
                return false;
            };

            Product[] resProductCode = Array.FindAll(productArray, ele => ele.code.Equals(newProduct.code));
            if (resProductCode.Length > 0)
            {
                labelAddProductMessage.Text = "Product Code is Duplicated!";
                labelAddProductMessage.ForeColor = Color.Yellow;
                return false;
            }

            if (newProduct.expiryDate.day == 0 || newProduct.expiryDate.month == 0 || newProduct.expiryDate.year == 0)
            {
                labelAddProductMessage.Text = "Please fill in field: Expiry Date!";
                labelAddProductMessage.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.expiryDate.day > 31 || newProduct.expiryDate.day < 1)
            {
                labelAddProductMessage.Text = "Please fill in field Expiry Day: Only 1 - 31 is valid value !";
                labelAddProductMessage.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.expiryDate.month > 12 || newProduct.expiryDate.day < 1)
            {
                labelAddProductMessage.Text = "Please fill in field Expiry Month: Only 1 - 12 is valid value !";
                labelAddProductMessage.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.expiryDate.year > 9000 || newProduct.expiryDate.year < 1000)
            {
                labelAddProductMessage.Text = "Please fill in field Expiry Year: Only 1000 - 9000 is valid value !";
                labelAddProductMessage.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.company == "")
            {
                labelAddProductMessage.Text = "Please fill in field: Company!";
                labelAddProductMessage.ForeColor = Color.Yellow;
                return false;
            };

            labelAddProductMessage.Text = "Add Product Successfully! Go to 'LIST product' Tab to see all of products";
            labelAddProductMessage.ForeColor = Color.SpringGreen;
            return true;
        }

        public void clearValueOfAddProductField()
        {
            textBoxAddProductName.Text = "";
            textBoxAddCategoryProduct.Text = "";
            textBoxAddCodeProduct.Text = "";
            numericUpDownDay.Text = "";
            numericUpDownMonth.Text = "";
            numericUpDownYear.Text = "";
            textBoxAddProductCompany.Text = "";
        }

        public void searchProductContainsProductName()
        {
            string searchProductName = textBoxSearchProduct.Text.Trim();
            if (searchProductName == "")
            {
                labelSearchValidation.Text = "Please fill in \"Product Name\" Field";
                labelAddProductMessage.ForeColor = Color.Yellow;
                listViewResultsProduct.Items.Clear();
            }
            else
            {
                labelSearchValidation.Text = "Search Done!";
                labelAddProductMessage.ForeColor = Color.SpringGreen;
                Product[] res = Array.FindAll(productArray, ele => ele.productName.Contains(searchProductName));
                listViewResultsProduct.Items.Clear();
                if (res.Length == 0)
                {
                    labelSearchValidation.Text = "No Existing Product";
                    labelSearchValidation.ForeColor = Color.Yellow;
                }
                else
                {
                    for (int i = 0; i < res.Length; i++)
                    {
                        ListViewItem itemSearchReSults = new ListViewItem();
                        itemSearchReSults.Text = res[i].productName;
                        itemSearchReSults.SubItems.Add(res[i].code);
                        itemSearchReSults.SubItems.Add(res[i].category);
                        itemSearchReSults.SubItems.Add(res[i].expiryDate.day.ToString()
                                            + "/" + res[i].expiryDate.month.ToString()
                                            + "/" + res[i].expiryDate.year.ToString());
                        itemSearchReSults.SubItems.Add(res[i].company);
                        listViewResultsProduct.Items.Add(itemSearchReSults);
                    }
                }
            }
        }

        public void searchProducEqualProduct()
        {
            string searchProductName = textBoxlabelProductNameSearchEditDeleteProduct.Text.Trim();

            if (searchProductName == "")
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "Please fill in mandatory fields above with exactly input!";
            }
            else
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "Search Done!";
            }

            Product[] resProductName = Array.FindAll(productArray, ele => ele.productName.Equals(searchProductName));

            if (resProductName.Length == 0)
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "No Existing Product";
                labelMessagelabelSearchEditDeleteProduct.ForeColor = Color.Yellow;
                textBoxSearchEditDeleteProductProductName.Text = "";
                textBoxSearchEditDeleteProductCode.Text = "";
                textBoxSearchEditDeleteProductCategory.Text = "";
                numericUpDownSearchEditDeleteProductExpiryDateDay.Text = "";
                numericUpDownSearchEditDeleteProductDayMonth.Text = "";
                numericUpDownSearchEditDeleteProductYear.Text = "";
                textBoxSearchEditDeleteProductCompnay.Text = "";
            }
            else
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "Search Done!";
                labelAddProductMessage.ForeColor = Color.SpringGreen;

                int indexProduct = Array.IndexOf(productArray, resProductName[0]);

                textBoxSearchEditDeleteProductProductName.Text = productArray[indexProduct].productName;
                textBoxSearchEditDeleteProductCode.Text = productArray[indexProduct].code;
                textBoxSearchEditDeleteProductCategory.Text = productArray[indexProduct].category;
                numericUpDownSearchEditDeleteProductExpiryDateDay.Text = productArray[indexProduct].expiryDate.day.ToString();
                numericUpDownSearchEditDeleteProductDayMonth.Text = productArray[indexProduct].expiryDate.month.ToString();
                numericUpDownSearchEditDeleteProductYear.Text = productArray[indexProduct].expiryDate.year.ToString();
                textBoxSearchEditDeleteProductCompnay.Text = productArray[indexProduct].company;
            }
        }

        public void deleteProduct()
        {
            string searchProductName = textBoxlabelProductNameSearchEditDeleteProduct.Text.Trim();

            if (searchProductName == "")
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "Please fill in mandatory fields above with exactly input!";
            }
            else
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "Search Done!";
            }

            Product[] resProductName = Array.FindAll(productArray, ele => ele.productName.Equals(searchProductName));

            if (resProductName.Length == 0)
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "No Existing Product";
                labelMessagelabelSearchEditDeleteProduct.ForeColor = Color.Yellow;
                
            }
            else
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "Search Done!";
                labelAddProductMessage.ForeColor = Color.SpringGreen;

                int indexProduct = Array.IndexOf(productArray, resProductName[0]);

                Product[] tempArray = new Product[productArray.Length - 1];
                for (int i = 0, j = 0; i < productArray.Length; i++)
                {
                    if (i == indexProduct)
                    {
                        continue;
                    }
                    tempArray[j] = productArray[i];
                    j++;
                }
                productArray = tempArray;

                textBoxSearchEditDeleteProductProductName.Text = "";
                textBoxSearchEditDeleteProductCode.Text = "";
                textBoxSearchEditDeleteProductCategory.Text = "";
                numericUpDownSearchEditDeleteProductExpiryDateDay.Text = "";
                numericUpDownSearchEditDeleteProductDayMonth.Text = "";
                numericUpDownSearchEditDeleteProductYear.Text = "";
                textBoxSearchEditDeleteProductCompnay.Text = "";

                displayAllProductOnList(productArray);
                syncCategoryArrayWithProductArray();
                displayAllCategoryOnList(categoryArray);

                labelSearchEditDeleteProductMessageEditDelete.Text = "Delete Sucessfully!";
                labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.SpringGreen;
            }
        }

        public void editProduct()
        {
            string searchProductName = textBoxlabelProductNameSearchEditDeleteProduct.Text.Trim();

            if (searchProductName == "")
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "Please fill in mandatory fields above with exactly input!";
            }
            else
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "Search Done!";
            }

            Product[] resProductName = Array.FindAll(productArray, ele => ele.productName.Equals(searchProductName));

            if (resProductName.Length == 0)
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "No Existing Product";
                labelMessagelabelSearchEditDeleteProduct.ForeColor = Color.Yellow;
            }
            else
            {
                labelMessagelabelSearchEditDeleteProduct.Text = "Search Done!";
                labelAddProductMessage.ForeColor = Color.SpringGreen;

                int indexProduct = Array.IndexOf(productArray, resProductName[0]);

                Product editProduct = getEditProduct();
                if (validateEditProduct(editProduct))
                {
                    productArray[indexProduct].productName = textBoxSearchEditDeleteProductProductName.Text.Trim();
                    productArray[indexProduct].code = textBoxSearchEditDeleteProductCode.Text.Trim();
                    productArray[indexProduct].category = textBoxSearchEditDeleteProductCategory.Text.Trim();
                    productArray[indexProduct].expiryDate.day = ((int)numericUpDownSearchEditDeleteProductExpiryDateDay.Value);
                    productArray[indexProduct].expiryDate.month = ((int)numericUpDownSearchEditDeleteProductDayMonth.Value);
                    productArray[indexProduct].expiryDate.year = ((int)numericUpDownSearchEditDeleteProductYear.Value);
                    productArray[indexProduct].company = textBoxSearchEditDeleteProductCompnay.Text.Trim();

                    displayAllProductOnList(productArray);
                    syncCategoryArrayWithProductArray();
                    displayAllCategoryOnList(categoryArray);

                    labelSearchEditDeleteProductMessageEditDelete.Text = "Edit Sucessfully!";
                    labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.SpringGreen;
                }
            }
        }

        public Product getEditProduct()
        {
            Product editProduct;
            editProduct.productName = textBoxSearchEditDeleteProductProductName.Text.Trim();
            editProduct.category = textBoxSearchEditDeleteProductCode.Text.Trim();
            editProduct.code = textBoxSearchEditDeleteProductCategory.Text.Trim();
            editProduct.expiryDate.day = ((int)numericUpDownSearchEditDeleteProductExpiryDateDay.Value);
            editProduct.expiryDate.month = ((int)numericUpDownSearchEditDeleteProductDayMonth.Value);
            editProduct.expiryDate.year = ((int)numericUpDownSearchEditDeleteProductYear.Value);
            editProduct.company = textBoxSearchEditDeleteProductCompnay.Text.Trim();
            return editProduct;
        }

        public bool validateEditProduct(Product newProduct)
        {
            if (newProduct.productName.Trim() == "")
            {
                labelSearchEditDeleteProductMessageEditDelete.Text = "Please fill in field: Product Name!";
                labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.category.Trim() == "")
            {
                labelSearchEditDeleteProductMessageEditDelete.Text = "Please fill in field: Category";
                labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.code.Trim() == "")
            {
                labelSearchEditDeleteProductMessageEditDelete.Text = "Please fill in field: Code!";
                labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.expiryDate.day == 0 || newProduct.expiryDate.month == 0 || newProduct.expiryDate.year == 0)
            {
                labelSearchEditDeleteProductMessageEditDelete.Text = "Please fill in field: Expiry Date!";
                labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.expiryDate.day > 31 || newProduct.expiryDate.day < 1)
            {
                labelSearchEditDeleteProductMessageEditDelete.Text = "Please fill in field Expiry Day: Only 1 - 31 is valid value !";
                labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.expiryDate.month > 12 || newProduct.expiryDate.day < 1)
            {
                labelSearchEditDeleteProductMessageEditDelete.Text = "Please fill in field Expiry Month: Only 1 - 12 is valid value !";
                labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.expiryDate.year > 9000 || newProduct.expiryDate.year < 1000)
            {
                labelSearchEditDeleteProductMessageEditDelete.Text = "Please fill in field Expiry Year: Only 1000 - 9000 is valid value !";
                labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.Yellow;
                return false;
            };

            if (newProduct.company.Trim() == "")
            {
                labelSearchEditDeleteProductMessageEditDelete.Text = "Please fill in field: Company!";
                labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.Yellow;
                return false;
            };

            labelSearchEditDeleteProductMessageEditDelete.Text = "Add Product Successfully! Go to 'LIST product' Tab to see all of products";
            labelSearchEditDeleteProductMessageEditDelete.ForeColor = Color.SpringGreen;
            return true;
        }

        public void displayAllCategoryOnList(string[] categoryArray)
        {
            string[] uniqueCategoryArray = categoryArray.Distinct().ToArray();
            listViewCategory.Items.Clear();
            for (int i = 0; i < uniqueCategoryArray.Length; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = uniqueCategoryArray[i];
                listViewCategory.Items.Add(item);
            }
        }

        public void syncCategoryArrayWithProductArray()
        {
            string[] newCategoryArrayFromProduct = new string[productArray.Length];
            for (int i = 0; i < productArray.Length; i++)
            {
                newCategoryArrayFromProduct[i] = productArray[i].category;
            }
            categoryArrayFromProduct = newCategoryArrayFromProduct;
            mergeCategoryWithCategoryNotFromProduct();
        }

        public void mergeCategoryWithCategoryNotFromProduct()
        {
            string[] newCategoryArray = new string[categoryArrayFromProduct.Length + categoryArrayNotFromProduct.Length];
            for (int i = 0; i < categoryArrayFromProduct.Length; i++)
            {
                newCategoryArray[i] = categoryArrayFromProduct[i];
            }
            for (int i = 0; i < categoryArrayNotFromProduct.Length; i++)
            {
                newCategoryArray[categoryArrayFromProduct.Length + i] = categoryArrayNotFromProduct[i];
            }
            categoryArray = newCategoryArray;
        }

        public void addCategory()
        {

            string newCategory = textBoxAddNewCategoty.Text.Trim();
            if (newCategory == "")
            {
                labelAddCaragoryMessage.Text = "Please fill in mandatory fields above!";
                labelAddCaragoryMessage.ForeColor = Color.Yellow;

            }
            else
            {
                string[] categoryExisting = Array.FindAll(categoryArray, ele => ele.Equals(newCategory));

                if (categoryExisting.Length > 0)
                {
                    labelAddCaragoryMessage.Text = "Category is Duplicated!";
                    labelAddCaragoryMessage.ForeColor = Color.Yellow;
                }
                else
                {
                    string[] newCategoryArrayNotFromProduct = new string[categoryArrayNotFromProduct.Length + 1];
                    for (int i = 0; i < categoryArrayNotFromProduct.Length; i++)
                    {
                        newCategoryArrayNotFromProduct[i] = categoryArrayNotFromProduct[i];
                    }
                    newCategoryArrayNotFromProduct[categoryArrayNotFromProduct.Length] = newCategory;
                    categoryArrayNotFromProduct = newCategoryArrayNotFromProduct;

                    mergeCategoryWithCategoryNotFromProduct();
                    displayAllCategoryOnList(categoryArray);

                    labelAddCaragoryMessage.Text = "Add Category Succesfully !";
                    labelAddCaragoryMessage.ForeColor = Color.SpringGreen;
                }
            }
           
        }

        public void searchCategoryContainsCategory()
        {
            string searchCategory = textBoxSearchCategory.Text.Trim();
            if (searchCategory == "")
            {
                labelSearchCategoryMessage.Text = "Please fill in \"Category\" Field";
                labelSearchCategoryMessage.ForeColor = Color.Yellow;
                listViewSerachResultCategory.Items.Clear();
            }
            else
            {
                labelSearchCategoryMessage.Text = "Search Done!";
                labelSearchCategoryMessage.ForeColor = Color.SpringGreen;

                listViewSerachResultCategory.Items.Clear();

                string[] resCategoryArray = Array.FindAll(categoryArray, ele => ele.Contains(searchCategory));

                if (resCategoryArray.Length == 0)
                {
                    labelSearchCategoryMessage.Text = "No Existing Category";
                    labelSearchCategoryMessage.ForeColor = Color.Yellow;
                    listViewSerachResultCategory.Items.Clear();

                }
                else
                {
                    string[] uniqueCategoryArray = resCategoryArray.Distinct().ToArray();

                    for (int i = 0; i < uniqueCategoryArray.Length; i++)
                    {
                        ListViewItem itemSearchReSults = new ListViewItem();
                        itemSearchReSults.Text = resCategoryArray[i];
                        listViewSerachResultCategory.Items.Add(itemSearchReSults);
                    }

                    labelSearchCategoryMessage.Text = "Search Done!";
                    labelSearchCategoryMessage.ForeColor = Color.SpringGreen;

                }
            }
        }

        public void searchCategoryEqualCategory()
        {
            string searchCategory = textBoxSearchEditDeleteCategory.Text.Trim();

            if (searchCategory == "")
            {
                labelSerachEditDeleteCategoryMessage.Text = "Please fill in mandatory fields above with exactly input!";
            }
            else
            {
                labelSerachEditDeleteCategoryMessage.Text = "Search Done!";
            }

            string[] resCategoryArrayFromProduct = Array.FindAll(categoryArrayFromProduct, ele => ele.Equals(searchCategory));
            string[] resCategoryArrayNotFromProduct = Array.FindAll(categoryArrayNotFromProduct, ele => ele.Equals(searchCategory));
            string[] resCategoryArray = Array.FindAll(categoryArray, ele => ele.Equals(searchCategory));

            if (resCategoryArray.Length == 0)
            {
                labelSerachEditDeleteCategoryMessage.Text = "No Existing Category";
                labelSerachEditDeleteCategoryMessage.ForeColor = Color.Yellow;
                listViewEditDeleteCategory.Items.Clear();
                textBoxResultSerachEditDeleteCategory.Text = "";


            }
            else
            {
                if (resCategoryArrayFromProduct.Length == 0)
                {
                    labelSerachEditDeleteCategoryMessage.Text = "Search Done!";
                    labelSerachEditDeleteCategoryMessage.ForeColor = Color.SpringGreen;
                    int indexCategory = Array.IndexOf(categoryArray, resCategoryArrayNotFromProduct[0]);
                    textBoxResultSerachEditDeleteCategory.Text = categoryArray[indexCategory];

                    listViewEditDeleteCategory.Items.Clear();

                }
                else
                {
                    labelSerachEditDeleteCategoryMessage.Text = "Search Done!";
                    labelSerachEditDeleteCategoryMessage.ForeColor = Color.SpringGreen;

                    int indexCategory = Array.IndexOf(categoryArray, resCategoryArrayFromProduct[0]);
                    textBoxResultSerachEditDeleteCategory.Text = categoryArray[indexCategory];

                    Product[] res = Array.FindAll(productArray, ele => ele.category.Equals(searchCategory));
                    listViewEditDeleteCategory.Items.Clear();
                    for (int i = 0; i < res.Length; i++)
                    {
                        ListViewItem itemSearchReSults = new ListViewItem();
                        itemSearchReSults.Text = res[i].productName;
                        itemSearchReSults.SubItems.Add(res[i].code);
                        itemSearchReSults.SubItems.Add(res[i].category);
                        itemSearchReSults.SubItems.Add(res[i].expiryDate.day.ToString()
                                            + "/" + res[i].expiryDate.month.ToString()
                                            + "/" + res[i].expiryDate.year.ToString());
                        itemSearchReSults.SubItems.Add(res[i].company);
                        listViewEditDeleteCategory.Items.Add(itemSearchReSults);
                    }
                }

            }
        }

        public void editCategory()
        {
            string searchCategory = textBoxSearchEditDeleteCategory.Text.Trim();
            string searchEditCategory = textBoxResultSerachEditDeleteCategory.Text.Trim();


            if (searchCategory == "") 
            {
                labelMessageEditDeleteCategory.Text = "Please fill in mandatory fields above with exactly input!";
            }
            else
            {
                string[] resCategoryArrayFromProduct = Array.FindAll(categoryArrayFromProduct, ele => ele.Equals(searchCategory));
                string[] resCategoryArrayNotFromProduct = Array.FindAll(categoryArrayNotFromProduct, ele => ele.Equals(searchCategory));
                string[] resCategoryArray = Array.FindAll(categoryArray, ele => ele.Equals(searchCategory));

                string[] resEditCategoryArray = Array.FindAll(categoryArray, ele => ele.Equals(searchEditCategory));

                if (resEditCategoryArray.Length != 0)
                {
                    labelMessageEditDeleteCategory.Text = "Category Duplicated";
                    labelMessageEditDeleteCategory.ForeColor = Color.Yellow;
                }
                else
                {
                    if (resCategoryArrayFromProduct.Length == 0)
                    {
                        labelMessageEditDeleteCategory.Text = "Edit Sucessfully!";
                        labelMessageEditDeleteCategory.ForeColor = Color.SpringGreen;


                        int indexCategory = Array.IndexOf(categoryArray, resCategoryArrayNotFromProduct[0]);
                        categoryArray[indexCategory] = textBoxResultSerachEditDeleteCategory.Text.Trim();

                        displayAllCategoryOnList(categoryArray);

                        listViewEditDeleteCategory.Items.Clear();

                        labelMessageEditDeleteCategory.Text = "Edit Sucessfully!";
                        labelMessageEditDeleteCategory.ForeColor = Color.SpringGreen;

                    }
                    else
                    {

                        int indexCategory = Array.IndexOf(categoryArray, resCategoryArrayFromProduct[0]);
                        categoryArray[indexCategory] = textBoxResultSerachEditDeleteCategory.Text;

                        Product[] res = Array.FindAll(productArray, ele => ele.category.Equals(searchCategory));
                        listViewEditDeleteCategory.Items.Clear();
                        for (int i = 0; i < res.Length; i++)
                        {
                            int productArrayIndex = Array.IndexOf(productArray, res[i]);
                            res[i].category = textBoxResultSerachEditDeleteCategory.Text.Trim();
                            productArray[productArrayIndex] = res[i];

                            displayAllProductOnList(productArray);
                            syncCategoryArrayWithProductArray();
                            displayAllCategoryOnList(categoryArray);

                            ListViewItem itemSearchReSults = new ListViewItem();
                            itemSearchReSults.Text = productArray[productArrayIndex].productName;
                            itemSearchReSults.SubItems.Add(productArray[productArrayIndex].code);
                            itemSearchReSults.SubItems.Add(productArray[productArrayIndex].category);
                            itemSearchReSults.SubItems.Add(productArray[productArrayIndex].expiryDate.day.ToString()
                                                + "/" + productArray[productArrayIndex].expiryDate.month.ToString()
                                                + "/" + productArray[productArrayIndex].expiryDate.year.ToString());
                            itemSearchReSults.SubItems.Add(productArray[productArrayIndex].company);
                            listViewEditDeleteCategory.Items.Add(itemSearchReSults);

                        }


                        labelMessageEditDeleteCategory.Text = "Edit Sucessfully!";
                        labelMessageEditDeleteCategory.ForeColor = Color.SpringGreen;
                    }

                }
            }

            
        }

        public void deleteCategory()
        {
            string searchCategory = textBoxSearchEditDeleteCategory.Text.Trim();

            if (searchCategory == "")
            {
                labelSerachEditDeleteCategoryMessage.Text = "Please fill in mandatory fields above with exactly input!";
            }
            else
            {
                labelSerachEditDeleteCategoryMessage.Text = "Search Done!";
            }

            string[] resCategoryArrayFromProduct = Array.FindAll(categoryArrayFromProduct, ele => ele.Equals(searchCategory));
            string[] resCategoryArrayNotFromProduct = Array.FindAll(categoryArrayNotFromProduct, ele => ele.Equals(searchCategory));
            string[] resCategoryArray = Array.FindAll(categoryArray, ele => ele.Equals(searchCategory));

            if (resCategoryArray.Length == 0)
            {
                labelSerachEditDeleteCategoryMessage.Text = "No Existing Product";
                labelSerachEditDeleteCategoryMessage.ForeColor = Color.Yellow;
            }
            else
            {
                if (resCategoryArrayFromProduct.Length == 0)
                {

                    int indexCategory = Array.IndexOf(categoryArrayNotFromProduct, resCategoryArrayNotFromProduct[0]);

                    string[] tempCategoryArray = new string[categoryArrayNotFromProduct.Length - 1];

                    for (int i = 0, j = 0; i < categoryArrayNotFromProduct.Length; i++)
                    {
                        if (i == indexCategory)
                        {
                            continue;
                        }
                        tempCategoryArray[j] = categoryArray[i];
                        j++;
                    }

                    tempCategoryArray.CopyTo(categoryArrayNotFromProduct, 0);

                    textBoxResultSerachEditDeleteCategory.Text = "";

                    syncCategoryArrayWithProductArray();
                    displayAllCategoryOnList(categoryArray);
                    labelMessageEditDeleteCategoryDelete.Text = "Delete Sucessfully!";
                    labelMessageEditDeleteCategoryDelete.ForeColor = Color.SpringGreen;
                }
                else
                {
                    Product[] res = Array.FindAll(productArray, ele => ele.category.Equals(searchCategory));
                    
                    for (int n = 0; n < res.Length; n++)
                    {
                        Product[] tempProductArray = new Product[productArray.Length - 1];
                        int productArrayIndex = Array.IndexOf(productArray, res[n]);
                        for (int i = 0, j = 0; i < productArray.Length; i++)
                        {
                            if (i == productArrayIndex)
                            {
                                continue;
                            }
                            tempProductArray[j] = productArray[i];
                            j++;
                        }
                        productArray = tempProductArray;
                    }

                    displayAllProductOnList(productArray);
                    syncCategoryArrayWithProductArray();
                    displayAllCategoryOnList(categoryArray);
                    labelMessageEditDeleteCategoryDelete.Text = "Delete Sucessfully!";
                    labelMessageEditDeleteCategoryDelete.ForeColor = Color.SpringGreen;

                    listViewEditDeleteCategory.Items.Clear();

                }
            }
        }

        
    }
}

