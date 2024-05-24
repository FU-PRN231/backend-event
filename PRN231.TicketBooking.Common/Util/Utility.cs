﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PRN231.TicketBooking.Common.Util
{
    public class Utility
    {
        public DateTime GetCurrentDateTimeInTimeZone()
        {
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            // Lấy thời gian hiện tại theo múi giờ địa phương của máy tính
            DateTime localTime = DateTime.Now;

            // Chuyển đổi thời gian địa phương sang múi giờ của Việt Nam
            DateTime vietnamTime = TimeZoneInfo.ConvertTime(localTime, vietnamTimeZone);

            return vietnamTime;
        }

        public string FormatMoney(double money)
        {
            return money.ToString("#,##0", System.Globalization.CultureInfo.InvariantCulture);
        }

        public string TranslateToVietnamese(double money)
        {
            if (money == 0) return "Không đồng";

            string[] unitNames = { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
            string[] digitNames = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

            long wholePart = (long)money;
            int index = 0;
            string result = "";

            while (wholePart > 0)
            {
                long chunk = wholePart % 1000;

                if (chunk > 0)
                    result = ConvertChunkToVietnamese(chunk, digitNames) + unitNames[index] + (result == "" ? "" : " ") + result;

                wholePart /= 1000;
                index++;
            }

            // Handle the decimal part
            string decimalPart = money.ToString("F2").Split('.')[1];
            if (decimalPart != "00")
            {
                result += " point";
                foreach (char digit in decimalPart)
                {
                    result += " " + digitNames[Convert.ToInt32(char.GetNumericValue(digit))];
                }
            }

            return result.Trim();
        }

        private static string ConvertChunkToVietnamese(long chunk, string[] digitNames)
        {
            int hundreds = (int)(chunk / 100), tens = (int)(chunk % 100 / 10), ones = (int)(chunk % 10);
            string result = "";

            if (hundreds > 0) result += digitNames[hundreds] + " trăm";
            if (tens > 1) result += (result == "" ? "" : " ") + digitNames[tens] + " mươi";
            else if (tens == 1) result += result == "" ? "" : " mười";
            if (ones > 1) result += (result == "" ? "" : " ") + digitNames[ones];
            else if (ones == 1) result += result == "" ? "" : " một";

            return result;
        }

        public string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        public double CaculateDiscount(double originPrice, double discount)
        {
            return (1 - discount / 100) * originPrice;
        }

        public DateTime GetCurrentDateInTimeZone()
        {
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            // Lấy thời gian hiện tại theo múi giờ địa phương của máy tính
            DateTime localTime = DateTime.Now;

            // Chuyển đổi thời gian địa phương sang múi giờ của Việt Nam
            DateTime vietnamTime = TimeZoneInfo.ConvertTime(localTime, vietnamTimeZone);

            return vietnamTime.Date;
        }

        private static HashSet<int> generatedNumbers = new HashSet<int>();

        public static int GenerateUniqueNumber()
        {
            while (true)
            {
                // Lấy thời gian hiện tại dưới dạng timestamp
                long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

                // Tạo số ngẫu nhiên
                Random random = new Random();
                int randomNumber = random.Next(1000, 10000);

                // Kết hợp thời gian và số ngẫu nhiên để tạo số nguyên dương
                int uniqueNumber = (int)(timestamp + randomNumber);

                // Kiểm tra xem số đã tồn tại chưa
                if (!generatedNumbers.Contains(uniqueNumber))
                {
                    generatedNumbers.Add(uniqueNumber);
                    return uniqueNumber;
                }
            }
        }

        public static List<T> ConvertIOrderQueryAbleToList<T>(IOrderedQueryable<T> list)
        {
            List<T> parseList = new List<T>();
            foreach (var item in list)
            {
                parseList.Add(item);
            }
            return parseList;
        }

        public static IOrderedQueryable<T> ConvertListToIOrderedQueryable<T>(List<T> list)
        {
            IOrderedQueryable<T> orderedQueryable = (IOrderedQueryable<T>)list.AsQueryable();
            return orderedQueryable;
        }

        public static string ReadAppSettingsJson()
        {
            var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            return File.ReadAllText(appSettingsPath);
        }

        public static void UpdateAppSettingValue(string section, string key, string value)
        {
            var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var json = File.ReadAllText(appSettingsPath);
            var settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            if (settings.ContainsKey(section) && settings[section] is JObject sectionObject)
            {
                if (sectionObject.ContainsKey(key))
                {
                    sectionObject[key] = value;
                    var updatedJson = JsonConvert.SerializeObject(settings, Formatting.Indented);
                    File.WriteAllText(appSettingsPath, updatedJson);
                }
            }
        }

        public static string ConvertToCronExpression(int hours, int minutes, int? day = null)
        {
            // Validate input
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59 || day.HasValue && (day.Value < 1 || day.Value > 31))
            {
                throw new ArgumentException("Invalid hours, minutes, or day");
            }

            // Hangfire cron expression format: "minute hour day * *"
            string cronExpression;

            if (day.HasValue)
            {
                cronExpression = $"{minutes} {hours} {day} * *";
            }
            else
            {
                // If day is not specified, use "?" to indicate no specific day
                cronExpression = $"{minutes} {hours} * * *";
            }

            return cronExpression;
        }

        public class FileChecker
        {
            public enum FileType
            {
                UNKNOWN,
                IsImage,
                IsVideo,
                IsPdf,
                IsWord,
                IsExcel
            }

            public static FileType CheckFileType(IFormFile file)
            {
                if (file == null || file.Length == 0)
                {
                    return FileType.UNKNOWN;
                }
                string fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (IsImage(fileExtension))
                {
                    return FileType.IsImage;
                }
                else if (IsVideo(fileExtension))
                {
                    return FileType.IsVideo;
                }
                else if (IsPdf(fileExtension))
                {
                    return FileType.IsPdf;
                }
                else if (IsWord(fileExtension))
                {
                    return FileType.IsWord;
                }
                else
                {
                    return FileType.UNKNOWN;
                }
            }

            private static bool IsImage(string fileExtension)
            {
                // Các định dạng ảnh được hỗ trợ
                string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                return Array.Exists(imageExtensions, ext => ext.Equals(fileExtension));
            }

            private static bool IsVideo(string fileExtension)
            {
                // Các định dạng video được hỗ trợ
                string[] videoExtensions = { ".mp4", ".avi", ".mkv", ".mov", ".wmv" };
                return Array.Exists(videoExtensions, ext => ext.Equals(fileExtension));
            }

            private static bool IsPdf(string fileExtension)
            {
                // Định dạng PDF
                return fileExtension.Equals(".pdf");
            }

            private static bool IsWord(string fileExtension)
            {
                // Các định dạng Word
                string[] wordExtensions = { ".doc", ".docx" };
                return Array.Exists(wordExtensions, ext => ext.Equals(fileExtension));
            }

            private static bool IsExcel(string fileExtension)
            {
                // Các định dạng Excel
                string[] excelExtensions = { ".xls", ".xlsx" };
                return Array.Exists(excelExtensions, ext => ext.Equals(fileExtension));
            }
        }
    }
}