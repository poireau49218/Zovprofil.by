﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;

namespace Zovprofil
{
    public class Framework {
        public static DateTime GetCurrentDate()
        {
            using (SqlDataAdapter DA = new SqlDataAdapter("SELECT GETDATE()", Catalog.ConnectionString))
            {
                using (DataTable DT = new DataTable())
                {
                    DA.Fill(DT);

                    return Convert.ToDateTime(DT.Rows[0][0]);
                }
            }
        }


        public static void SetLog(string Page, string ip)
        {
            try
            {
                using (SqlDataAdapter DA = new SqlDataAdapter("SELECT TOP 0 * FROM infiniu2_light.dbo.SiteLog", Catalog.ConnectionString))
                {
                    using (SqlCommandBuilder CB = new SqlCommandBuilder(DA))
                    {
                        using (DataTable DT = new DataTable())
                        {
                            DA.Fill(DT);

                            DataRow NewRow = DT.NewRow();
                            NewRow["Page"] = Page;
                            NewRow["DateTime"] = GetCurrentDate();
                            NewRow["IPAddress"] = ip;
                            DT.Rows.Add(NewRow);

                            DA.Update(DT);
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }

    public class Catalog
    {
        public static string ConnectionString = "Data Source=localhost;Initial Catalog=infiniu2_catalog;Persist Security Info=True;Connection Timeout=30;User ID=infiniu2_infinium;Password=InF476()*";
        public static string ftpPath = "ftp://localhost/Documents/TechStoreDocuments/";
        public static string ftp = "ftp://localhost";

        //public static string ConnectionString = "Data Source=185.204.118.40, 32433;Initial Catalog=infiniu2_catalog;Persist Security Info=True;Connection Timeout=30;User ID=infiniu2_infinium;Password=InF476()*";
        //public static string ftpPath = "ftp://infinium.zovprofil.by/Documents/TechStoreDocuments/";
        //public static string ftp = "ftp://infinium.zovprofil.by";


        public static string URL = "https://zovprofil.by/Images/ClientsCatalogImages/";

        public static string ftpUsername = "infiniu2_infinium";
        public static string ftpPassword = "vqju]nkca8ygtfibrQop";

        public static DataTable FillCategories(int Type)
        {
            using (SqlDataAdapter DA = new SqlDataAdapter("SELECT DISTINCT(Category) FROM ClientsCatalogImages WHERE (Category IS NOT NULL AND Category <> '' AND Category NOT LIKE '%Эксклюзив ZOV%') AND (ProductType = " + Type + ") AND ToSite = 1", ConnectionString))
            {
                using (DataTable DT = new DataTable())
                {
                    DA.Fill(DT);
                    DT.Columns.Add("FileName", System.Type.GetType("System.String"));

                    foreach (DataRow Row in DT.Rows)
                    {
                        using (SqlDataAdapter sDA = new SqlDataAdapter("SELECT TOP 1 FileName FROM ClientsCatalogImages WHERE ProductType = " + Type + " AND Category = '" + Row["Category"].ToString() + "' AND ToSite = 1 AND ProductType != 3", ConnectionString))
                        {
                            using (DataTable sDT = new DataTable())
                            {
                                sDA.Fill(sDT);
                                Row["FileName"] = sDT.Rows[0]["FileName"];
                            }
                        }
                    }

                    return DT;
                }
            }
        }

        public static DataTable FillProducts(int Type, string Category)
        {
            string Select = @"SELECT FileName, Name, Description, Material, Sizes, Color, ImageID 
                                FROM ClientsCatalogImages 
                                WHERE ProductType = @type AND Category = @category AND ToSite = 1 AND CatSlider = 0 AND MainSlider = 0 " +
                                "ORDER BY Name ASC";
            if (Type == 0)
                Select = @"SELECT FileName, Name, Description, Material, Sizes, Color, ImageID 
                            FROM ClientsCatalogImages 
                            WHERE ProductType = @type AND Category = @category AND ToSite = 1 AND CatSlider = 0 AND MainSlider = 0 AND Basic = 1 
                            ORDER BY Color ASC";
            else if(Type == 1)
                Select = @"SELECT FileName, c.Name, Description, Material, Sizes, Color, ImageID 
                            FROM ClientsCatalogImages as c
                            INNER JOIN(
	                            SELECT Name, MIN(Color) AS FirstColor, MIN(FileName) as FileN
	                            FROM ClientsCatalogImages 
	                            WHERE ProductType = @type AND ToSite = 1 AND CatSlider = 0 AND MainSlider = 0 AND Category = @category
	                            GROUP BY NAME
                            ) as t ON c.Name=t.Name and c.Color=t.FirstColor
                            WHERE ProductType = @type AND ToSite = 1 AND CatSlider = 0 AND MainSlider = 0 AND Category = @category 
                            ORDER BY c.Name";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Select, connection))
                {
                    command.Parameters.AddWithValue("@type", Type);
                    command.Parameters.AddWithValue("@category", Category);
                    using (SqlDataAdapter DA = new SqlDataAdapter(command))
                    {
                        using (DataTable DT = new DataTable())
                        {
                            DA.Fill(DT);
                            return DT;
                        }
                    }
                }
            }
        }

        public static DataTable FillBasicDecors(string Category)
        {
            string Select = @"SELECT Name, FileName, 
                                CASE 
                                    WHEN CHARINDEX(' ', Name) > 0 THEN SUBSTRING(Name, 1, CHARINDEX(' ', Name) - 1)
                                    ELSE Name
                                END AS unique_name
                            FROM (
                                SELECT *, ROW_NUMBER() OVER (PARTITION BY 
                                    CASE 
                                        WHEN CHARINDEX(' ', Name) > 0 THEN SUBSTRING(Name, 1, CHARINDEX(' ', Name) - 1)
                                        ELSE Name
                                    END
                                    ORDER BY color) as rn
                                FROM ClientsCatalogImages
                                WHERE ProductType = 1 AND Category LIKE @category AND ToSite = 1 AND CatSlider = 0 AND MainSlider = 0
                            ) as UniqueUsers
                            WHERE rn = 1
                            ORDER BY unique_name";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Select, connection))
                {
                    command.Parameters.AddWithValue("@category", Category);
                    using (SqlDataAdapter DA = new SqlDataAdapter(command))
                    {
                        using (DataTable DT = new DataTable())
                        {
                            DA.Fill(DT);
                            return DT;
                        }
                    }
                }
            }
        }

        public static DataTable FillNotBasicDecors(string name, string category)
        {
            string Select = @"SELECT FileName, Name, Description, Color, ImageID 
                                FROM ClientsCatalogImages 
                                WHERE ProductType = 1 AND Category LIKE @category AND ToSite = 1 AND CatSlider = 0 AND MainSlider = 0
                                    AND (
                                        (Name LIKE @name AND LEN(Name) = LEN(@name)) OR
                                        (Name LIKE @name + ' %')
                                    )
                                ORDER BY Color ASC";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Select, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@category", category);
                    using (SqlDataAdapter DA = new SqlDataAdapter(command))
                    {
                        using (DataTable DT = new DataTable())
                        {
                            DA.Fill(DT);
                            return DT;
                        }
                    }
                }
            }
        }

        public static DataTable FillNewProducts()
        {
            using (SqlDataAdapter DA = new SqlDataAdapter("SELECT FileName, Name, Color, ImageID, Category, ProductType FROM ClientsCatalogImages WHERE Latest = 1 AND ToSite = 1", ConnectionString))
            {
                using (DataTable DT = new DataTable())
                {
                    DA.Fill(DT);

                    return DT;
                }
            }
        }

        public static DataTable FillMainSliderImages()
        {
            using (SqlDataAdapter DA = new SqlDataAdapter("SELECT FileName, Name, MainSliderLink FROM ClientsCatalogImages WHERE MainSlider = 1 AND ToSite = 1", ConnectionString))
            {
                using (DataTable DT = new DataTable())
                {
                    DA.Fill(DT);

                    return DT;
                }
            }
        }

        public static DataTable FillCatSliderImages(string Category)
        {
            using (SqlDataAdapter DA = new SqlDataAdapter("SELECT FileName, Name, Description FROM ClientsCatalogImages WHERE Category = '" + Category + "' AND CatSlider = 1 AND ToSite = 1", ConnectionString))
            {
                using (DataTable DT = new DataTable())
                {
                    DA.Fill(DT);

                    return DT;
                }
            }
        }

        /*public static void GetItemDetail(int ImageID, ref string FileName, ref string Name, ref string Description, ref string Material, ref string Sizes, ref string Basic)
        {
            using (SqlDataAdapter DA = new SqlDataAdapter("SELECT FileName, Name, Description, Material, Sizes, Basic FROM ClientsCatalogImages WHERE ImageID = " + ImageID, ConnectionString))
            {
                using (DataTable DT = new DataTable())
                {
                    DA.Fill(DT);

                    FileName = DT.Rows[0]["FileName"].ToString();
                    Name = DT.Rows[0]["Name"].ToString();
                    Description = DT.Rows[0]["Description"].ToString();
                    Material = DT.Rows[0]["Material"].ToString();
                    Sizes = DT.Rows[0]["Sizes"].ToString();
                    Basic = DT.Rows[0]["Basic"].ToString();
                }
            }
        }*/

        public static void GetItemDetail(int ImageID, ref string FileName, ref string Name, ref string Description, ref string Material, ref string Sizes, ref string ConfigID, ref string ProductType, ref string TechID, ref string Color, ref string Basic, ref string Category, ref string PatinaID, ref string ColorID)
        {

            //using (SqlDataAdapter DA = new SqlDataAdapter("SELECT FileName, Name, Description, Material, Sizes, ConfigID, ProductType FROM ClientsCatalogImages WHERE ImageID = " + ImageID, ConnectionString))
            using (SqlDataAdapter DA = new SqlDataAdapter("SELECT FileName, " +
                                                                    "Name, " +
                                                                    "Category, " +
                                                                    "Description, " +
                                                                    "Material, " +
                                                                    "Sizes, " +
                                                                    "CI.ConfigID, " +
                                                                    "ProductType, " +
                                                                    "Color, " +
                                                                    "FrontID, " +
                                                                    "DecorID, " +
                                                                    "Basic, " +
                                                                    "CF.PatinaID AS CFPatinaID, " +
                                                                    "CF.ColorID AS CFColorID, " +
                                                                    "CD.PatinaID AS CDPatinaID, " +
                                                                    "CD.ColorID  AS CDColorID " +
                                                            "FROM ClientsCatalogImages as CI " +
                                                            "LEFT JOIN [infiniu2_catalog].[dbo].[ClientsCatalogFrontsConfig] as CF ON CI.ConfigID = CF.ConfigID " +
                                                            "LEFT JOIN [infiniu2_catalog].[dbo].[ClientsCatalogDecorConfig] as CD ON CI.ConfigID = CD.ConfigID " +
                                                            "WHERE ImageID = " + ImageID, ConnectionString))
            {

                using (DataTable DT = new DataTable())
                {
                    DA.Fill(DT);

                    FileName = DT.Rows[0]["FileName"].ToString();
                    Name = DT.Rows[0]["Name"].ToString();
                    Description = DT.Rows[0]["Description"].ToString();
                    Material = DT.Rows[0]["Material"].ToString();
                    Sizes = DT.Rows[0]["Sizes"].ToString();
                    ConfigID = DT.Rows[0]["ConfigID"].ToString();
                    ProductType = DT.Rows[0]["ProductType"].ToString();
                    Color = DT.Rows[0]["Color"].ToString();
                    Basic = DT.Rows[0]["Basic"].ToString();
                    Category = DT.Rows[0]["Category"].ToString();

                    if (ProductType == "0")
                    {
                        TechID = DT.Rows[0]["FrontID"].ToString();
                        PatinaID = DT.Rows[0]["CFPatinaID"].ToString();
                        ColorID = DT.Rows[0]["CFColorID"].ToString();
                    }
                    else
                    {
                        TechID = DT.Rows[0]["DecorID"].ToString();
                        PatinaID = DT.Rows[0]["CDPatinaID"].ToString();
                        ColorID = DT.Rows[0]["CDColorID"].ToString();
                    }
                }
            }
        }

        public static string SerializeDT(DataTable DT)
        {
            string res = "";

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                res += "{";

                for (int j = 0; j < DT.Columns.Count; j++)
                {
                    res += "[" + DT.Columns[j].ColumnName + "]";
                    res += "=" + DT.Rows[i][j].ToString() + ";";
                }

                res += "}";
            }

            return res;
        }








        public static void ProcessProductImage(string sourceImagePath, string destinationImagePath)
        {
            if (!CheckFileExists(destinationImagePath))
            {
                // Загрузка большой картинки с FTP-сервера
                byte[] imageData = DownloadImageFromFtp(sourceImagePath);

                // Уменьшение картинки
                if (imageData != null)
                {
                    byte[] resizedImageData = ResizeImage(imageData, 0.85, 0.85);

                    // Сохранение уменьшенной картинки на FTP-сервере
                    UploadImageToFtp(destinationImagePath, resizedImageData);
                }
                    

                
            }
        }

        public static bool CheckFileExists(string filePath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + filePath);
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
                else
                    throw;
            }
        }

        private static byte[] DownloadImageFromFtp(string filePath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + filePath);
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    responseStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            catch
            {
                return null;
            }
            
        }

        private static byte[] ResizeImage(byte[] imageData, double widthRatio, double heightRatio)
        {
            using (MemoryStream sourceStream = new MemoryStream(imageData))
            using (Image sourceImage = Image.FromStream(sourceStream))
            {
                int newWidth = sourceImage.Width;
                int newHeight = sourceImage.Height;
                while (Math.Max(newWidth, newHeight) > 450)
                {
                    newWidth = (int)(newWidth * widthRatio);
                    newHeight = (int)(newHeight * heightRatio);
                }
                    

                using (Bitmap resizedImage = new Bitmap(newWidth, newHeight))
                using (Graphics graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;

                    graphics.DrawImage(sourceImage, 0, 0, newWidth, newHeight);

                    using (MemoryStream destinationStream = new MemoryStream())
                    {
                        resizedImage.Save(destinationStream, sourceImage.RawFormat);
                        return destinationStream.ToArray();
                    }
                }
            }
        }

        private static void UploadImageToFtp(string filePath, byte[] imageData)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + filePath);
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(imageData, 0, imageData.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload complete, status: {response.StatusDescription}");
            }
        }











        public static string GetTechStoreImage(int TechStoreID)
        {
            using (SqlDataAdapter DA = new SqlDataAdapter("SELECT * FROM TechStoreDocuments" +
                " WHERE DocType = 0 AND TechID = " + TechStoreID, ConnectionString))
            {
                using (DataTable DT = new DataTable())
                {
                    if (DA.Fill(DT) == 0)
                        //return "pict_stub.png";
                        return null;

                    if (DT.Rows[0]["FileName"] == DBNull.Value)
                        //return "pict_stub.png";
                        return null;

                    string FileName = DT.Rows[0]["FileName"].ToString();
                    string FileSize = DT.Rows[0]["FileSize"].ToString();
                    string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\TechStore", FileName);

                    FileInfo fi = new FileInfo(FilePath);

                    if (File.Exists(FilePath) && fi.Length == Convert.ToInt32(FileSize))
                        return FileName;

                    if (File.Exists(FilePath))
                        File.Delete(FilePath);
                    try
                    {
                        string savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\TechStore", FileName);

                        // Создаем объект запроса FTP
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpPath + FileName);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                        // Устанавливаем данные для авторизации на FTP-сервере
                        request.Credentials = new NetworkCredential("infiniu2_infinium", "vqju]nkca8ygtfibrQop");

                        // Получаем ответ от FTP-сервера
                        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        {
                            // Получаем поток ответа
                            using (Stream responseStream = response.GetResponseStream())
                            {
                                // Создаем файловый поток для сохранения изображения
                                using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
                                {
                                    // Считываем данные из потока ответа и записываем их в файловый поток
                                    byte[] buffer = new byte[1024];
                                    int bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                                    while (bytesRead > 0)
                                    {
                                        fileStream.Write(buffer, 0, bytesRead);
                                        bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                                    }
                                }
                            }
                        }
                        return FileName;
                    }
                    catch (Exception ex)
                    {
                        string filePath = @"D:\log\FTPlog.txt";
                        string content = DateTime.Now + "\r\n" + ftpPath + FileName + "\r\n" + ex.ToString() + "\r\n" + "--------------------------" + "\r\n";

                        // создание файла (если его нет)
                        if (!File.Exists(filePath))
                            File.Create(filePath).Close();

                        // запись в файл (если он уже существует)
                        using (StreamWriter sw = File.AppendText(filePath))
                        {
                            // дописываем информацию
                            sw.Write(content);
                        }

                        //MessageBox.Show(ex.ToString());

                        return "pict_stub.png";
                    }
                }
            }
        }

        // Возвращает MatrixID фасада по ConfigID из таблицы ClientCatalogImages
        public static void GetMatrixIdFromConfID(int ConfigID, ref int MatrixID)
        {
            string Select = "SELECT MatrixId " +
                            "FROM [infiniu2_catalog].[dbo].[FrontsConfig] as FC " +
                            "LEFT JOIN [ClientsCatalogFrontsConfig] AS CCF " +
                                "ON CCF.FrontID = FC.FrontID " +
                                "AND CCF.ColorID = FC.ColorID " +
                                "AND CCF.InsetTypeID = FC.InsetTypeID " +
                                "AND CCF.PatinaID = FC.PatinaID " +
                                "AND CCF.InsetColorID = FC.InsetColorID " +
                            "WHERE ConfigID = @configid AND Enabled = 1";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Select, conn);
                cmd.Parameters.Add("@configid", SqlDbType.Int);
                cmd.Parameters["@configid"].Value = ConfigID;

                try
                {
                    conn.Open();
                    MatrixID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MatrixID = 0;
                }
            }
        }


        // Создает таблицу декоров, связанных с фасадом с переданным MatrixID
        public static DataTable FillRelatedDecors(int matrixid)
        {
            string Select = "SELECT DISTINCT [ImageID], [FileName], [ToSite], [Category], [Name], [Color], [Basic] " +
                            "FROM CollectionsConfig " +
                            "INNER JOIN DecorConfig " +
                                "ON CollectionsConfig.ConfigId2 = DecorConfig.MatrixID " +
                            "INNER JOIN ClientsCatalogDecorConfig " +
                                "ON DecorConfig.DecorID = ClientsCatalogDecorConfig.DecorID " +
                                "AND ClientsCatalogDecorConfig.ColorID = DecorConfig.ColorID " +
                                "AND ClientsCatalogDecorConfig.PatinaID = DecorConfig.PatinaID " +
                            "INNER JOIN ClientsCatalogImages " +
                                "ON ClientsCatalogDecorConfig.ConfigID = ClientsCatalogImages.ConfigID " +
                            "WHERE CollectionsConfig.ConfigId1 = @MatrixID AND ToSite = 1 and ProductType = 1 ORDER BY Category";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Select, connection))
                {

                    SqlParameter matrID = new SqlParameter("@MatrixID", matrixid);
                    command.Parameters.Add(matrID);

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    da.Dispose();

                    return dt;
                }
            }
        }


        public static DataTable FillNotBasicFronts(int MatrixID)
        {
            string Select = "SELECT DISTINCT[ImageID], [FileName], [ProductType], [ToSite], [Category], [Name], [Color], [Basic]" +
                            "FROM CollectionsConfig " +
                            "INNER JOIN FrontsConfig " +
                                "ON CollectionsConfig.ConfigId2 = FrontsConfig.MatrixID " +
                            "INNER JOIN ClientsCatalogFrontsConfig " +
                                "ON FrontsConfig.FrontID = ClientsCatalogFrontsConfig.FrontID " +
                                    "AND ClientsCatalogFrontsConfig.ColorID = FrontsConfig.ColorID " +
                                    "AND ClientsCatalogFrontsConfig.PatinaID = FrontsConfig.PatinaID " +
                                    "AND ClientsCatalogFrontsConfig.InsetColorID = FrontsConfig.InsetColorID " +
                                    "AND ClientsCatalogFrontsConfig.InsetTypeID = FrontsConfig.InsetTypeID " +
                            "INNER JOIN ClientsCatalogImages " +
                                "ON ClientsCatalogFrontsConfig.ConfigID = ClientsCatalogImages.ConfigID " +
                            "WHERE CollectionsConfig.ConfigId1 = @MatrixID AND ProductType = 0 AND ToSite = 1 AND Category NOT LIKE '%Эксклюзив%'";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(Select, connection))
                {
                    connection.Open();
                    SqlParameter mtrx = new SqlParameter("@MatrixID", MatrixID);

                    command.Parameters.Add(mtrx);

                    using (SqlDataAdapter DA = new SqlDataAdapter(command))
                    {
                        using (DataTable DT = new DataTable())
                        {
                            DA.Fill(DT);
                            return DT;
                        }
                    }
                }
            }
        }
        
        // Проверяет существование файла по ссылке
        public static bool IsFileExist(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (WebException ex)
            {
                return false;
            }
        }

    }
}