using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using File = System.IO.File;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.VisualBasic;

namespace Telegram.Bot.Examples.Echo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Run().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine("Error!");
                Console.ReadKey();
            }
        }

        static void SaveSource_Destination(string Tables,string chat_ids, string codes)
        {
            string cul = "";
            if (Tables == "Mabda") cul = "Sources";
            if (Tables == "Maghsad") cul = "Destinations";
            DBKernel k = new DBKernel();
            string s = "";
            s = "" +
                " INSERT INTO " + Tables + " " + "\n" +
                "           (Chat_ID " + "\n" +
                "           ," + cul + " ) " + "\n" +
                "     VALUES " + "\n" +
                "           ( '" + chat_ids + "'" + "\n" +
                "           ,N'" + codes + "')" + "\n";
            k.ExecuteCommand_DB(s);
        }

        static void Save_User_Selected_City(string chat_ids , string  city)
        {
            DBKernel k = new DBKernel();
            string s = "" +
                " INSERT INTO User_Selected_City " + "\n" +
                "           (Chat_ID " + "\n" +
                "           ,City_Msg ) " + "\n" +
                "     VALUES " + "\n" +
                "           ( '" + chat_ids + "'" + "\n" +
                "           ,N'" + city + "')" + "\n";
            k.ExecuteCommand_DB(s);
        }

        static void Delete_User_Selected_City(string chat_ids)
        {
            DBKernel k = new DBKernel();
            string s = "" +
                " DELETE FROM User_Selected_City WHERE Chat_ID = '" + chat_ids + "' " + "\n";
            k.ExecuteCommand_DB(s);
        }

        static string Get_User_Selected_City(string chat_ids)
        {
            string rval = "";
            DBKernel k = new DBKernel();
            string s = "" +
                " SELECT City_Msg FROM User_Selected_City WHERE Chat_ID = '" + chat_ids  + "' " + "\n";
            rval = k.Get_ExecuteCommand_DB(s);
            return rval;
        }

        static async Task Run()
        {
            var Bot = new Api("180073139:AAH9---otKv3cN0DGwMZ15RDN0bhThskuok");

            var me = await Bot.GetMe();

            Console.WriteLine("Hello my name is {0}", me.Username);

            var offset = 0;

            while (true)
            {
                var updates = await Bot.GetUpdates(offset);

                Telegram.Bot.Types.ReplyKeyboardMarkup rkm = new ReplyKeyboardMarkup();

                rkm.Keyboard = new string[][]
                    {
                            new string[] {"تهران","مشهد","شیراز"},
                            new string[] {"اصفهان","کیش"},
                            new string[] {"پیشنهاد ویژه"}
                    };
                rkm.OneTimeKeyboard = true;
                rkm.Selective = true;
                rkm.ResizeKeyboard = true;

                foreach (var update in updates)
                {
                    
                    if (update.Message.Type == MessageType.TextMessage)
                    {

                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Chat ID: {0}", update.Message.Chat.Id);
                        Console.WriteLine("Echo Message: {0}", update.Message.Text);
                        Console.WriteLine("Chat First Name: {0}",update.Message.Chat.FirstName);
                        Console.WriteLine("Chat Last Name: {0}",update.Message.Chat.LastName);
                        Console.WriteLine("Chat Title: {0}", update.Message.Chat.Title);
                        Console.WriteLine("Chat UserName: {0}",update.Message.Chat.Username);
                        Console.WriteLine("Chat Date: {0}", DateTime.Now.ToString());
                        Console.WriteLine("------------------------------------");

                        string sql_query = "" +
                        " INSERT INTO Users " + "\n" +
                        "           (Chat_ID " + "\n" +
                        "           ,Echo_Message " + "\n" +
                        "           ,Chat_First_Name " + "\n" +
                        "           ,Chat_Last_Name " + "\n" +
                        "           ,Chat_Title " + "\n" +
                        "           ,Chat_UserName " + "\n" +
                        "           ,Chat_Date " + "\n" +
                        "           ,Chat_Time) " + "\n" +
                        "     VALUES " + "\n" +
                        "           ( '" + update.Message.Chat.Id        + "'" + "\n" +
                        "           ,N'" + update.Message.Text           + "'" + "\n" +
                        "           ,N'" + update.Message.Chat.FirstName + "'" + "\n" +
                        "           ,N'" + update.Message.Chat.LastName  + "'" + "\n" +
                        "           ,N'" + update.Message.Chat.Title     + "'" + "\n" +
                        "           ,N'" + update.Message.Chat.Username  + "'" + "\n" +
                        "           , '" + DateTime.Now.ToString("yyyy/MM/dd") + "'" + "\n" +
                        "           , '" + DateTime.Now.ToString("HH:mm")+ "')" + "\n";

                        DBKernel k = new DBKernel();
                        k.ExecuteCommand_DB(sql_query);

                        //await Bot.SendChatAction(update.Message.Chat.Id, ChatAction.Typing);
                        //await Task.Delay(2000);

                        if (update.Message.Chat.Id == 79157286)
                        {
                            if (update.Message.Text.ToLower() == "tabligh")
                            {
                                string sTabligh;
                                sTabligh = "سلام"
                                    + "\n" + "من ربات جستجوگر بلیط های چارتری هستم"
                                    + "\n" + ""
                                    + "\n" + "شما می توانید به کمک من"
                                    + "\n" + "بلیط های هواپیما با قیمت مناسب و ارزان تهیه کنید"
                                    + "\n" + ""
                                    + "\n" + ""
                                    + "\n" + "با مراجعه به کانال ما، از لیست ارزانترین پرواز ها در مسیر های مختلف مطلع شوید"
                                    + "\n" + "https://telegram.me/charterticketonline"
                                    + "\n" + ""
                                    + "\n" + ""
                                    + "\n" + "اگر از خدمات من رضایت داشتید"
                                    + "\n" + "من را به دوستانتان پیشنهاد دهید"
                                    + "\n" + ""
                                    + "\n" + "لطفا شهر مبدا و مقصد را به ترتیب انتخاب کنید"
                                ;
                                //    + "\n" + "فقط کافی است کلمه"
                                //    + "\n" + "start"
                                //    + "\n" + " را لمس کنید "
                                //;
                                //long cid = 321453184;

                                //for (long i = 11234567; i <= cid; cid++)
                                //{
                                //    try
                                //    {
                                //        var t = await Bot.SendTextMessage(cid, sTabligh, false, 0, rkm);
                                //        Console.WriteLine("-----------------------------------------");
                                //        Console.WriteLine("erasal shod be " + cid);
                                //        Console.WriteLine("-----------------------------------------");
                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        Console.WriteLine("bot cant send text message ."
                                //          + "\n" + "tabligh ersal nashod"
                                //          + "\n" + ex.Message + "\n" + ex.Source);
                                //    }
                                //}
                                //Data Source=SAFASOFTCO\SQL2014;Initial Catalog=sepehr360;User ID=sa;Password=***********
                                DBKernel.con.ConnectionString = "Data Source=safasoftco\\SQL2014;Initial Catalog=sepehr360;User ID=sa;Password=951753";
                                DBKernel.con.Open();
                                DBKernel.com.Connection = DBKernel.con;
                                DBKernel.com.CommandText = "" +
                                " SELECT [Chat_ID] FROM [sepehr360].[dbo].[Users] GROUP BY [Chat_ID] " + "\n";

                                DBKernel.dReader = DBKernel.com.ExecuteReader();
                                while (DBKernel.dReader.Read() == true)
                                {
                                    long cid = 0;
                                    cid = long.Parse(DBKernel.dReader["Chat_ID"] + "" );

                                    try
                                    {
                                        var t = await Bot.SendTextMessage(cid, sTabligh, false, 0, rkm);
                                        Console.WriteLine("-----------------------------------------");
                                        Console.WriteLine("erasal shod be " + cid);
                                        Console.WriteLine("-----------------------------------------");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("bot cant send text message ."
                                          + "\n" + "tabligh ersal nashod"
                                          + "\n" + ex.Message + "\n" + ex.Source);
                                    }
                                }
                                DBKernel.dReader.Close();
                                DBKernel.con.Close();
                            }
                        }

                        if (update.Message.Text.ToLower() == "hi" || update.Message.Text.ToLower() == "/start" || update.Message.Text == "سلام" )
                        {

                            string strHelp;
                            strHelp = "سلام"
                                + " " + update.Message.Chat.FirstName + " "
                                + "\n" + "من ربات جستجوگر بلیط های چارتری هستم  ..."
                                + "\n" + ""
                                + "\n" + "برای آشنایی با کارایی من عبارت"
                                + "\n" + "کمک"
                                + "\n" + "یا"
                                + "\n" + "help"
                                + "\n" + "ارسال کن";

                            try
                            { var t = await Bot.SendTextMessage(update.Message.Chat.Id, strHelp, false, update.Message.MessageId, rkm); }
                            catch (Exception ex)
                            { Console.WriteLine("bot cant send text message ." 
                                + "\n" + "ersal start / hi / salam"
                                + "\n" + ex.Message + "\n" + ex.Source); }
                           // var t = await Bot.SendTextMessage(update.Message.Chat.Id, strHelp);
                        }

                        if (update.Message.Text.ToLower() == "help" || update.Message.Text == "کمک")
                        {
                            
                            string strHelp;
                            strHelp = "ابتدا یکی از شهر های ذیل را به عنوان شهر مبدا انتخاب کنید." 
                                + "\n" + "سپس شهر مقصد را انتخاب کنید."
                                + "\n" + "در انتها با انتخاب لینک بلیط خود را رزرو نمایید.";
                            try
                            { var t = await Bot.SendTextMessage(update.Message.Chat.Id, strHelp, false, update.Message.MessageId, rkm); }
                            catch (Exception ex)
                            {
                                Console.WriteLine("bot cant send text message ."
                                    + "\n" + "ersal komak / help "
                                    + "\n" + ex.Message + "\n" + ex.Source);
                            }
                        }

                        if (update.Message.Text.ToLower() == "پیشنهاد ویژه")
                        {
                            /*
                                    SELECT  ROW_NUMBER() OVER (ORDER BY Setareh DESC) , [Matn]  FROM [Pishnahadat] WHERE ([Hotels] = 1 ) AND ([dtStart] >= '1395/03/01' ) AND ([dtEnd] <= '1395/03/31') 
                                    UNION ALL
                                    SELECT  ROW_NUMBER() OVER (ORDER BY Setareh DESC) , [Matn]  FROM [Pishnahadat] WHERE (Photographys = 1 ) AND ([dtStart] >= '1395/03/01' ) AND ([dtEnd] <= '1395/03/31') 
                                    UNION ALL
                                    SELECT  ROW_NUMBER() OVER (ORDER BY Setareh DESC) , [Matn]  FROM [Pishnahadat] WHERE ([Resturants] = 1 ) AND ([dtStart] >= '1395/03/01' ) AND ([dtEnd] <= '1395/03/31') 
                                    UNION ALL
                                    SELECT  ROW_NUMBER() OVER (ORDER BY Setareh DESC) , [Matn]  FROM [Pishnahadat] WHERE ([Entertaiment] = 1 ) AND ([dtStart] >= '1395/03/01' ) AND ([dtEnd] <= '1395/03/31') 
                            */
                            DBKernel.con.ConnectionString = DBKernel.pdp;
                            DBKernel.con.Open();
                            DBKernel.com.Connection = DBKernel.con;
                            DBKernel.com.CommandText = "" +
                                " SELECT  ROW_NUMBER() OVER (ORDER BY Setareh DESC) , [Matn]  FROM [Pishnahadat] WHERE ([Hotels] = 1 ) AND ([dtStart] <= '" + dt.Get_Tarikh() + "' ) AND ([dtEnd] >= '" + dt.Get_Tarikh() + "') " +
                                " UNION ALL" +
                                " SELECT  ROW_NUMBER() OVER (ORDER BY Setareh DESC) , [Matn]  FROM [Pishnahadat] WHERE ([Photographys] = 1 ) AND ([dtStart] <= '" + dt.Get_Tarikh() + "' ) AND ([dtEnd] >= '" + dt.Get_Tarikh() + "') " +
                                " UNION ALL" +
                                " SELECT  ROW_NUMBER() OVER (ORDER BY Setareh DESC) , [Matn]  FROM [Pishnahadat] WHERE ([Resturants] = 1 ) AND ([dtStart] <= '" + dt.Get_Tarikh() + "' ) AND ([dtEnd] >= '" + dt.Get_Tarikh() + "') " +
                                " UNION ALL" +
                                " SELECT  ROW_NUMBER() OVER (ORDER BY Setareh DESC) , [Matn]  FROM [Pishnahadat] WHERE ([Entertaiment] = 1 ) AND ([dtStart] <= '" + dt.Get_Tarikh() + "' ) AND ([dtEnd] >= '" + dt.Get_Tarikh() + "') " +
                                "";

                            string sTabligh = "";

                            DBKernel.dReader = DBKernel.com.ExecuteReader();
                            while (DBKernel.dReader.Read() == true)
                            {

                                sTabligh += DBKernel.dReader["Matn"] +
                                    "\n" +
                                    "\n" + "---- ---- ----" +
                                    "\n";
                            }
                            DBKernel.dReader.Close();
                            DBKernel.con.Close();

                            try
                            {
                                var t = await Bot.SendTextMessage(update.Message.Chat.Id, sTabligh, false, 0, rkm);
                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine("Tabligh erasal shod be " + update.Message.Chat.Id);
                                Console.WriteLine("-----------------------------------------");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("bot cant send text message ."
                                  + "\n" + "tabligh ersal nashod"
                                  + "\n" + ex.Message + "\n" + ex.Source);
                            }
                        }
                        ///*
                        string ss = Get_User_Selected_City(update.Message.Chat.Id + "");
                        string dd = "";

                        if ((ss == "THR") || (ss == "MHD") ||
                            (ss == "IFN") || (ss == "KIH") ||
                            (ss == "SYZ"))
                        //if ((update.Message.Text == "تهران") || (update.Message.Text == "مشهد") ||
                        //    (update.Message.Text == "اصفهان") || (update.Message.Text == "کیش") ||
                        //    (update.Message.Text == "شیراز"))
                        {
                            //maghsad
                            if (update.Message.Text.ToLower() == "تهران")
                            {
                                dd = "THR";
                            }

                            if (update.Message.Text.ToLower() == "مشهد")
                            {
                                dd = "MHD";
                            }

                            if (update.Message.Text.ToLower() == "شیراز")
                            {
                                dd = "SYZ";
                            }
                            if (update.Message.Text.ToLower() == "اصفهان")
                            {
                                dd = "IFN";
                            }

                            if (update.Message.Text.ToLower() == "کیش")
                            {
                                dd = "KIH";
                            }

                            if ((dd == "THR") || (dd == "MHD") ||
                            (dd == "IFN") || (dd == "KIH") ||
                            (dd == "SYZ"))
                            {
                                SaveSource_Destination("Maghsad", update.Message.Chat.Id + "", dd);
                                string matn = "";
                                matn = "شهر مقصد شما " + update.Message.Text + " می باشد." + "\n" + "\n" + "منتظر باشید.";
                                try
                                { var t = await Bot.SendTextMessage(update.Message.Chat.Id, matn, false, update.Message.MessageId, rkm); }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("bot cant send text message ."
                                        + "\n" + "ersal maghsad "
                                        + "\n" + ex.Message + "\n" + ex.Source);
                                }


                                for (int i = 0; i <= 2; i++)
                                {
                                    string sdt = "";

                                    if (i == 0)
                                        sdt = dt.Get_Tarikh();
                                    else
                                        sdt = dt.Get_NextDay_s(dt.Get_Tarikh(), i);

                                    //SELECT ID,mabda,maghsad,link,software,tedad,ghimat,airline,saat,flight_number,fdate,fdatepers,airplain,agency,toz,tarikh,tm  FROM sepehr360.dbo.Ticket
                                    DBKernel.con.ConnectionString = DBKernel.pdp;
                                    DBKernel.con.Open();
                                    DBKernel.com.Connection = DBKernel.con;
                                    DBKernel.com.CommandText = "" +
                                    " SELECT TOP 1 * , PARSENAME(CONVERT(VARCHAR, CAST((ghimat + 5000)  AS MONEY), 1),2) as nerkh  FROM Ticket " + "\n" +
                                    " WHERE mabda = '" + ss + "' AND maghsad = '" + dd + "' AND fdatepers = '" + sdt + "'  " + "\n" +
                                    " AND ghimat = (SELECT MIN(ghimat) FROM Ticket WHERE mabda = '" + ss + "' AND maghsad = '" + dd + "' AND fdatepers = '" + sdt + "' ) " + "\n";

                                    DBKernel.dReader = DBKernel.com.ExecuteReader();
                                    while (DBKernel.dReader.Read() == true)
                                    {

                                        string az = "";
                                        string be = "";

                                        switch (ss)
                                        {
                                            case "THR":
                                                az = "تهران";
                                                break;
                                            case "MHD":
                                                az = "مشهد";
                                                break;
                                            case "SYZ":
                                                az = "شیراز";
                                                break;
                                            case "IFN":
                                                az = "اصفهان";
                                                break;
                                            case "KIH":
                                                az = "کیش";
                                                break;
                                        }

                                        switch (dd)
                                        {
                                            case "THR":
                                                be = "تهران";
                                                break;
                                            case "MHD":
                                                be = "مشهد";
                                                break;
                                            case "SYZ":
                                                be = "شیراز";
                                                break;
                                            case "IFN":
                                                be = "اصفهان";
                                                break;
                                            case "KIH":
                                                be = "کیش";
                                                break;
                                        }

                                        string msg_Text = "";
                                        msg_Text += "پرواز چارتری از " + az + " به " + be + " : " + "\n";
                                        msg_Text += "ساعت  = " + DBKernel.dReader["saat"] + "\n";
                                        msg_Text += "تاریخ  = " + DBKernel.dReader["fdatepers"] + "\n";
                                        msg_Text += "تعداد جای خالی  = " + DBKernel.dReader["tedad"] + "\n";
                                        msg_Text += "قیمت  = " + DBKernel.dReader["nerkh"] + " تومان " + "\n";
                                        msg_Text += "(ایرلاین)هواپیمایی  = " + DBKernel.dReader["airline"] + "\n";
                                        msg_Text += "شماره پرواز  = " + DBKernel.dReader["flight_number"] + "\n";
                                        msg_Text += "شماره تماس  = 09354184487";
                                        try
                                        { var t = await Bot.SendTextMessage(update.Message.Chat.Id, msg_Text, false, update.Message.MessageId, rkm); }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("bot cant send text message ."
                                                + "\n" + "ersal etelaat parvaz ha "
                                                + "\n" + ex.Message + "\n" + ex.Source);
                                        }
                                    }
                                    DBKernel.dReader.Close();
                                    DBKernel.con.Close();

                                    Delete_User_Selected_City(update.Message.Chat.Id + "");
                                }
                            }
                        }
                        else
                        {
                            //mabda
                            if (update.Message.Text.ToLower() == "تهران")
                            {
                                ss = "THR";
                            }
                            if (update.Message.Text.ToLower() == "مشهد")
                            {
                                ss = "MHD";
                            }

                            if (update.Message.Text.ToLower() == "شیراز")
                            {
                                ss = "SYZ";
                            }

                            if (update.Message.Text.ToLower() == "اصفهان")
                            {
                                ss = "IFN";
                            }

                            if (update.Message.Text.ToLower() == "کیش")
                            {
                                ss = "KIH";
                            }

                            if ((ss == "THR") || (ss == "MHD") ||
                            (ss == "IFN") || (ss == "KIH") ||
                            (ss == "SYZ"))
                            {
                                SaveSource_Destination("Mabda", update.Message.Chat.Id + "", ss);
                                string matn;
                                matn = "شهر مبدا شما " + update.Message.Text + " می باشد." + "\n" + "حالا شهر مقصد خود را انتخاب نمایید.";
                                try
                                { var t = await Bot.SendTextMessage(update.Message.Chat.Id, matn, false, update.Message.MessageId, rkm); }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("bot cant send text message ."
                                        + "\n" + "ersal mabda "
                                        + "\n" + ex.Message + "\n" + ex.Source);
                                }
                                Save_User_Selected_City(update.Message.Chat.Id + "", ss);
                            }
                        }


 //*/
                    }

   
                    //if (update.Message.Type == MessageType.PhotoMessage)
                    //{
                    //    var file = await Bot.GetFile(update.Message.Photo.LastOrDefault()?.FileId);
                        
                    //    Console.WriteLine("Received Photo: {0}", file.FilePath);

                    //    var filename = file.FileId+"."+file.FilePath.Split('.').Last();

                    //    using (var profileImageStream = File.Open(filename, FileMode.Create))
                    //    {
                    //        await file.FileStream.CopyToAsync(profileImageStream);
                    //    }
                    //}

                    offset = update.Id + 1;
                }

                await Task.Delay(1000);
            }
        }
    }

    class dt
    {
        static PersianCalendar PC = new PersianCalendar();

        public static string Get_NextDay_s(string tarikh, int tedad)
        {
            DateTime d;
            d = PC.ToDateTime(int.Parse(Strings.Mid(tarikh, 1, 4)), int.Parse(Strings.Mid(tarikh, 6, 2)), int.Parse(Strings.Mid(tarikh, 9, 2)), 0, 0, 0, 0);
            return to_Shamsi(PC.AddDays(d, tedad).ToString("yyyy/MM/dd"));
        }

        public static string Get_Tarikh_Miladi()
        {

            DateTime date1 = DateTime.Now;
            string str = date1.ToString("yyyy/MM/dd");
            return str;
        }

        public static string to_Miladi(string strdate)
        {
            string ret = "";
            DateTime d;
            if (strdate.Length == 10)
            {
                d = PC.ToDateTime(Convert.ToInt32(strdate.Substring(0, 4)), Convert.ToInt32(strdate.Substring(5, 2)), Convert.ToInt32(strdate.Substring(8, 2)), 0, 0, 0, 0);
                ret = d.ToString("yyyy/MM/dd");
            }
            return ret;
        }
        public static string to_Shamsi(string strdate)
        {
            //    Dim ret As String = ""
            //If Len(strdate) = 10 Then
            //    ret = MakeDate(pc.GetYear(strdate) & "/" & pc.GetMonth(strdate) & "/" & pc.GetDayOfMonth(strdate))
            //    'ret = d.ToString("yyyy/MM/dd")
            //End If

            //Return ret

            string ret = "";
            DateTime d = Convert.ToDateTime(strdate);
            DateTime s;
            if (strdate.Length == 10)
            {
                ret = MakeDate(Convert.ToString(PC.GetYear(d)) + "/" + Convert.ToString(PC.GetMonth(d)) + "/" + Convert.ToString(PC.GetDayOfMonth(d)));
                //ret = s.ToString("yyyy/MM/dd");
            }
            return ret;
        }

        public static string Get_Tarikh()
        {
            string y = "";
            string m = "";
            string d = "";
            string ret = "";
            DateTime t;
            y = Convert.ToString(PC.GetYear(DateTime.Now));
            m = Convert.ToString(PC.GetMonth(DateTime.Now));
            d = Convert.ToString(PC.GetDayOfMonth(DateTime.Now));
            //t = Convert.ToDateTime(y + "/" + m + "/" + d);
            ret = MakeDate(y + "/" + m + "/" + d);
            //ret = t.ToString("yyyy/MM/dd");
            return ret;
        }
        public static string Get_Year()
        {
            string y = "";
            string m = "";
            string d = "";
            string ret = "";
            DateTime t;
            y = Convert.ToString(PC.GetYear(DateTime.Now));
            m = Convert.ToString(PC.GetMonth(DateTime.Now));
            d = Convert.ToString(PC.GetDayOfMonth(DateTime.Now));
            t = Convert.ToDateTime(y + "/" + m + "/" + d);
            ret = t.ToString("yyyy");
            return ret;
        }
        public static string Get_Month()
        {
            string y = "";
            string m = "";
            string d = "";
            string ret = "";
            DateTime t;
            y = Convert.ToString(PC.GetYear(DateTime.Now));
            m = Convert.ToString(PC.GetMonth(DateTime.Now));
            d = Convert.ToString(PC.GetDayOfMonth(DateTime.Now));
            t = Convert.ToDateTime(y + "/" + m + "/" + d);
            ret = t.ToString("MM");
            return ret;
        }
        public static string Get_Day()
        {
            string y = "";
            string m = "";
            string d = "";
            string ret = "";
            DateTime t;
            y = Convert.ToString(PC.GetYear(DateTime.Now));
            m = Convert.ToString(PC.GetMonth(DateTime.Now));
            d = Convert.ToString(PC.GetDayOfMonth(DateTime.Now));
            t = Convert.ToDateTime(y + "/" + m + "/" + d);
            ret = t.ToString("dd");
            return ret;
        }

        public static string get_Mah_Name()
        {
            int val = PC.GetMonth(DateTime.Now);
            string ret = "";
            switch (val)
            {
                case 1:
                    ret = "فروردین";
                    break;
                case 2:
                    ret = "اردیبهشت";
                    break;
                case 3:
                    ret = "خرداد";
                    break;
                case 4:
                    ret = "تیر";
                    break;
                case 5:
                    ret = "مرداد";
                    break;
                case 6:
                    ret = "شهریور";
                    break;
                case 7:
                    ret = "مهر";
                    break;
                case 8:
                    ret = "آبان";
                    break;
                case 9:
                    ret = "آذر";
                    break;
                case 10:
                    ret = "دی";
                    break;
                case 11:
                    ret = "بهمن";
                    break;
                case 12:
                    ret = "اسفند";
                    break;
            }
            return ret;
        }
        public static string get_Rooz_Mah()
        {
            int val = PC.GetMonth(DateTime.Now);
            string ret = "";
            switch (val)
            {
                case 1:
                    ret = "31";
                    break;
                case 2:
                    ret = "31";
                    break;
                case 3:
                    ret = "31";
                    break;
                case 4:
                    ret = "31";
                    break;
                case 5:
                    ret = "31";
                    break;
                case 6:
                    ret = "31";
                    break;
                case 7:
                    ret = "30";
                    break;
                case 8:
                    ret = "30";
                    break;
                case 9:
                    ret = "30";
                    break;
                case 10:
                    ret = "30";
                    break;
                case 11:
                    ret = "30";
                    break;
                case 12:
                    ret = "30";
                    break;
            }
            return ret;
        }
        public static string get_Day_Name(int numDay)
        {
            //    Dim d As Integer
            //    d = Date.Now.DayOfWeek
            string ret = "";
            switch (numDay)
            {
                case 6:
                    ret = "شنبه";
                    break;
                case 0:
                    ret = "یک شنبه";
                    break;
                case 1:
                    ret = "دو شنبه";
                    break;
                case 2:
                    ret = "سه شنبه";
                    break;
                case 3:
                    ret = "چهار شنبه";
                    break;
                case 4:
                    ret = "پنج شنبه";
                    break;
                case 5:
                    ret = "جمعه";
                    break;
            }

            return ret;
        }

        public static string Get_Time()
        {
            string h = "";
            string m = "";
            string s = "";
            string ret = "";
            DateTime t;
            h = Convert.ToString(DateTime.Now.Hour);
            m = Convert.ToString(DateTime.Now.Minute);
            s = Convert.ToString(DateTime.Now.Second);
            t = Convert.ToDateTime(h + ":" + m + ":" + s);
            ret = t.ToString("HH:mm:ss");
            return ret;
        }
        public static string Get_Hour()
        {
            string h = "";
            string m = "";
            string s = "";
            string ret = "";
            DateTime t;
            h = Convert.ToString(DateTime.Now.Hour);
            m = Convert.ToString(DateTime.Now.Minute);
            s = Convert.ToString(DateTime.Now.Second);
            t = Convert.ToDateTime(h + ":" + m + ":" + s);
            ret = t.ToString("HH");
            return ret;
        }
        public static string Get_Minute()
        {
            string h = "";
            string m = "";
            string s = "";
            string ret = "";
            DateTime t;
            h = Convert.ToString(DateTime.Now.Hour);
            m = Convert.ToString(DateTime.Now.Minute);
            s = Convert.ToString(DateTime.Now.Second);
            t = Convert.ToDateTime(h + ":" + m + ":" + s);
            ret = t.ToString("mm");
            return ret;
        }
        public static string Get_Second()
        {
            string h = "";
            string m = "";
            string s = "";
            string ret = "";
            DateTime t;
            h = Convert.ToString(DateTime.Now.Hour);
            m = Convert.ToString(DateTime.Now.Minute);
            s = Convert.ToString(DateTime.Now.Second);
            t = Convert.ToDateTime(h + ":" + m + ":" + s);
            ret = t.ToString("ss");
            return ret;
        }

        public static string Next_Month(string dt)
        {
            int y = Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(dt, 1, 4));
            int m = Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(dt, 6, 2));
            int d = Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(dt, 9, 2));

            if ((m + 1) <= 12)
                m += 1;
            else
            {
                y += 1;
                m = 1;
            }

            return MakeDate(y + "/" + m + "/" + d);
        }
        public static bool CheckDate(string strDate)
        {
            bool blnvalue = false;

            string strval = "";

            strval = Microsoft.VisualBasic.Strings.Mid(strDate, 5, 1);

            if (strval != "/")
                blnvalue = false;

            strval = Microsoft.VisualBasic.Strings.Mid(strDate, 6, 2);

            if (Convert.ToInt32(strval) > 12)
                blnvalue = false;

            strval = Microsoft.VisualBasic.Strings.Mid(strDate, 8, 1);

            if (strval != "/")
                blnvalue = false;

            strval = Microsoft.VisualBasic.Strings.Mid(strDate, 9, 2);

            if (Convert.ToInt32(strval) > 31)
                blnvalue = false;

            return blnvalue;
        }
        public static bool CheckDate(string strNasb, string strKharid)
        {
            bool blnvalue = true;

            if ((strNasb.Length < 10) || (strKharid.Length < 10))
                return false;

            string y1Strval, m1Strval, d1Strval, y2Strval, m2Strval, d2Strval;

            y1Strval = Microsoft.VisualBasic.Strings.Mid(strNasb, 1, 4);
            y2Strval = Microsoft.VisualBasic.Strings.Mid(strKharid, 1, 4);
            m1Strval = Microsoft.VisualBasic.Strings.Mid(strNasb, 6, 2);
            m2Strval = Microsoft.VisualBasic.Strings.Mid(strKharid, 6, 2);
            d1Strval = Microsoft.VisualBasic.Strings.Mid(strNasb, 9, 2);
            d2Strval = Microsoft.VisualBasic.Strings.Mid(strKharid, 9, 2);

            if (Convert.ToInt32(y1Strval) < (Convert.ToInt32(y2Strval)))
                blnvalue = false;
            if (Convert.ToInt32(m1Strval) < (Convert.ToInt32(m2Strval)))
                blnvalue = false;
            if (Convert.ToInt32(d1Strval) < (Convert.ToInt32(d2Strval)))
                blnvalue = false;

            return blnvalue;
        }
        public static string MakeDate(string strDate)
        {
            int kol, len1, len2;
            string strval, k;
            string blnvalue;
            strval = strDate;
            do
            {
                if (strval.Length == 0)
                    return strval;
                kol = strval.Length;
                len1 = Microsoft.VisualBasic.Strings.InStr(strval, "/");
                k = Microsoft.VisualBasic.Strings.Mid(strval, len1 + 1, (kol - len1));
                len2 = Microsoft.VisualBasic.Strings.InStr(k, "/");
                blnvalue = Convert.ToString(len1 & len2);
                if (len1 == 3)
                    strval = "13" + strval;
                if ((len1 == 5) && (len2 == 2))
                    strval = strval.Insert(len1, "0");
                if ((len1 == 5) && (len2 == 3) && (strval.Length == 9))
                    strval = strval.Insert(len1 + len2, "0");
            } while (strval.Length < 10);
            return strval;
        }
        public static string Tafazol_Tarikh(string strdt1, string strdt2)
        {
            string ret = "";
            PersianCalendar p = new PersianCalendar();
            strdt1 = Convert.ToString(p.ToDateTime(Convert.ToInt32(strdt1.Substring(0, 4)), Convert.ToInt32(strdt1.Substring(5, 2)), Convert.ToInt32(strdt1.Substring(8, 2)), 0, 0, 0, 0));
            strdt2 = Convert.ToString(p.ToDateTime(Convert.ToInt32(strdt2.Substring(0, 4)), Convert.ToInt32(strdt2.Substring(5, 2)), Convert.ToInt32(strdt2.Substring(8, 2)), 0, 0, 0, 0));
            DateTime dt1 = DateTime.Parse(strdt1);
            DateTime dt2 = DateTime.Parse(strdt2);
            TimeSpan t = dt2 - dt1;
            ret = Convert.ToString(t.Days);
            return ret;
        }
    }

    class DBKernel
    {

        public static string pdp = "Data Source=safasoftco\\sql0864;Initial Catalog=sepehr360;User ID=sa;Password=951753";
        //'Dim pdp As String = String.Format("Data Source=RAMEANI-PC;Initial Catalog=sepehr360;Integrated Security=True")
        //Dim pdp As String = String.Format("Data Source=safasoftco\sql0864;Initial Catalog=sepehr360;User ID=sa;Password=951753")
        public static SqlConnection con = new SqlConnection();
        public static SqlCommand com = new SqlCommand();
        public static SqlDataAdapter dAdapter = new SqlDataAdapter();
        public static SqlDataReader dReader;
        public static DataSet dSet = new DataSet();

        public void Conncetion_DB(string ConString)
        {
            try
            {
                con.Close();
                con.ConnectionString = ConString;
                con.Open();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.Source);
                //Application.Exit();
            }
        }
        public void Command_DB(string SqlCommand, string TableName)
        {
            try
            {

                Conncetion_DB(pdp);
                dSet.Clear();
                com.Connection = con;
                com.CommandText = SqlCommand;
                com.CommandTimeout = 0;

                dAdapter.SelectCommand = com;
                dAdapter.Fill(dSet, TableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.Source);
                con.Close();
            }
        }
        public void ExecuteCommand_DB(string SqlCommand)
        {
            try
            {

                Conncetion_DB(pdp);
                dSet.Clear();
                com.Connection = con;
                com.CommandText = SqlCommand;
                com.CommandTimeout = 0;

                com.ExecuteReader();

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.Source);
                con.Close();
            }
        }
        public string Get_ExecuteCommand_DB(string SqlCommand)
        {
            try
            {

                Conncetion_DB(pdp);
                dSet.Clear();
                com.Connection = con;
                com.CommandText = SqlCommand;
                com.CommandTimeout = 0;

                string strval = "";
                strval = com.ExecuteScalar().ToString();
                com.ExecuteReader();

                con.Close();
                return strval;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.Source);
                con.Close();
                return "0";
            }

        }
    }
}
