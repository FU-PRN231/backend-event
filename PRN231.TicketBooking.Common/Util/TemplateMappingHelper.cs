using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Text;

namespace PRN231.TicketBooking.Common.Util
{
    public class TemplateMappingHelper
    {
        public enum ContentEmailType
        {
            VERIFICATION_CODE,
            FORGOTPASSWORD,
            CONTRACT_CODE,
            SPONSOR_ACCOUNT_CREATION
        }

        public static string GetTemplateOTPEmail(ContentEmailType type, string body, string name)
        {
            string content = "";
            switch (type)
            {
                case ContentEmailType.VERIFICATION_CODE:
                    content = @"
<html>
  <head>
    <style>
      * {
        margin: 0;
        padding: 0;
      }

      body {
        font-family: Arial, sans-serif;
        background-color: #f4f4f4; /* Background color for the entire email */
      }

      .container {
        max-width: 900px;
        margin: 20 auto;
        /* padding: 20px; */
        border-radius: 5px;
        box-shadow: 0px 0px 5px 2px #ccc; /*Add a shadow to the content */
      }

      .header {
        text-align: center;
        background-color: #ffba00; /* Header background color */
        padding: 20px;
      }
      .header-title {
        text-align: left;
        background-color: #2ad65e; /* Header background color */
        padding: 20px;
        color: white;
      }
      .title {
        color: black; /* Text color for the title */
        font-size: 30px;
        font-weight: bold;
      }

      .greeting {
        font-size: 18px;
        margin: 10 5;
      }
      .emailBody {
        margin: 5 5;
      }
      .support {
        font-size: 15px;
        font-style: italic;
        margin: 5 5;
      }

      .mainBody {
        background-color: #ffffff; /* Main content background color */
        padding: 20px;
        /* border-radius: 5px; */
        /* box-shadow: 0px 0px 5px 2px #ccc; Add a shadow to the content */
      }
      .body-content {
        /* display: flex;
        flex-direction: column; */
        border: 1px #fff8ea;
        border-radius: 5px;
        margin: 10 5;
        padding: 10px;
        /* background-color: #fff8ea; */
        box-shadow: 0px 0px 5px 2px #ccc;
      }
      .title-content {
        font-weight: bold;
      }

      u i {
        color: blue;
      }

      .footer {
        font-size: 14px;
        text-align: center;
        background-color: #ffba00; /* Header background color */
        padding: 10px;
        display: flex;
        justify-content: center;
        flex-direction: column;
      }
      .footer-text {
        font-weight: 600;
      }
      .signature {
        text-align: right;
        font-size: 16px;
        margin: 5 5;
      }
    </style>
  </head>
  <body>
    <div class=""container"">
      <div
        style=""
          height: 100px;
          display: flex;
          align-items: center;
          justify-content: center;
          background-color: white;
        ""
      >
        <p
          style=""
            color: #515151;
            text-align: center;
            margin: auto 0;
            font-size: 30px;
          ""
        >
          Cóc Event
        </p>
      </div>
      <div class=""mainBody"">
        <!-- <div class=""header-title"">
        </div> -->
        <h2 class=""emailBody"">Hello " + name + @" ,</h2>
        <p class=""greeting""></p>

        <p class=""emailBody"">
          You are currently registering an account through <b><i>Cóc Event </i></b>.
        </p>
        <p class=""emailBody"">
          Below is your OTP information:
          <b><i> " + body + @"</i></b>
        </p>

        <p class=""emailBody"">
          Please enter the code above into the system to proceed to the next step
          <a href=""https://lovehouse.vercel.app/""
            ><span style=""font-weight: bold; text-transform: uppercase""
              >here</span
            ></a
          >
        </p>
        <p class=""support"">
          Thank you for your interest in the services of <b><i>Cóc Event</i></b
          >, for any inquiries, please contact
          <u><i>qk.backend@gmail.com</i></u> for support
        </p>
        <div class=""signature"">
          <p>Best regards,</p>
          <p>
            <b><i>Cóc Event Team</i></b>
          </p>
        </div>
      </div>
      <div style=""height: 100px"">

      </div>
    </div>
  </body>
</html>

";
                    break;

                case ContentEmailType.CONTRACT_CODE:
                    content = @"
<html>
  <head>
    <style>
      * {
        margin: 0;
        padding: 0;
      }

      body {
        font-family: Arial, sans-serif;
        background-color: #f4f4f4; /* Background color for the entire email */
      }

      .container {
        max-width: 900px;
        margin: 20 auto;
        /* padding: 20px; */
        border-radius: 5px;
        box-shadow: 0px 0px 5px 2px #ccc; /*Add a shadow to the content */
      }

      .header {
        text-align: center;
        background-color: #ffba00; /* Header background color */
        padding: 20px;
      }
      .header-title {
        text-align: left;
        background-color: #2ad65e; /* Header background color */
        padding: 20px;
        color: white;
      }
      .title {
        color: black; /* Text color for the title */
        font-size: 30px;
        font-weight: bold;
      }

      .greeting {
        font-size: 18px;
        margin: 10 5;
      }
      .emailBody {
        margin: 5 5;
      }
      .support {
        font-size: 15px;
        font-style: italic;
        margin: 5 5;
      }

      .mainBody {
        background-color: #ffffff; /* Main content background color */
        padding: 20px;
        /* border-radius: 5px; */
        /* box-shadow: 0px 0px 5px 2px #ccc; Add a shadow to the content */
      }
      .body-content {
        /* display: flex;
        flex-direction: column; */
        border: 1px #fff8ea;
        border-radius: 5px;
        margin: 10 5;
        padding: 10px;
        /* background-color: #fff8ea; */
        box-shadow: 0px 0px 5px 2px #ccc;
      }
      .title-content {
        font-weight: bold;
      }

      u i {
        color: blue;
      }

      .footer {
        font-size: 14px;
        text-align: center;
        background-color: #ffba00; /* Header background color */
        padding: 10px;
        display: flex;
        justify-content: center;
        flex-direction: column;
      }
      .footer-text {
        font-weight: 600;
      }
      .signature {
        text-align: right;
        font-size: 16px;
        margin: 5 5;
      }
    </style>
  </head>
  <body>
    <div class=""container"">
      <div
        style=""
          height: 100px;
          display: flex;
          align-items: center;
          justify-content: center;
          background-color: white;
        ""
      >
        <p
          style=""
            color: #515151;
            text-align: center;
            margin: auto 0;
            font-size: 30px;
          ""
        >
          Cóc Event
        </p>
      </div>
      <div class=""mainBody"">
        <!-- <div class=""header-title"">
        </div> -->
        <h2 class=""emailBody"">Hello " + name + @" ,</h2>
        <p class=""greeting""></p>

        <p class=""emailBody"">
          You are in the process of completing contract procedures through <b><i>Cóc Event </i></b>.
        </p>
        <p class=""emailBody"">
          Below is your OTP information:
          <b><i> " + body + @"</i></b>
        </p>

        <p class=""emailBody"">
          Please enter the code above into the system to proceed to the next step
          <a href=""https://lovehouse.vercel.app/""
            ><span style=""font-weight: bold; text-transform: uppercase""
              >here</span
            ></a
          >
        </p>
        <p class=""support"">
          Thank you for your interest in the services of <b><i>Cóc Event</i></b
          >, for any inquiries, please contact
          <u><i>qk.backend@gmail.com</i></u> for support
        </p>
        <div class=""signature"">
          <p>Best regards,</p>
          <p>
            <b><i>Cóc Event Team</i></b>
          </p>
        </div>
      </div>
      <div style=""height: 100px"">

      </div>
    </div>
  </body>
</html>

";
                    break;

                case ContentEmailType.FORGOTPASSWORD:
                    content = @"
<html>
  <head>
    <style>
      * {
        margin: 0;
        padding: 0;
      }

      body {
        font-family: Arial, sans-serif;
        background-color: #f4f4f4; /* Background color for the entire email */
      }

      .container {
        max-width: 900px;
        margin: 20 auto;
        /* padding: 20px; */
        border-radius: 5px;
        box-shadow: 0px 0px 5px 2px #ccc; /*Add a shadow to the content */
      }

      .header {
        text-align: center;
        background-color: #ffba00; /* Header background color */
        padding: 20px;
      }
      .header-title {
        text-align: left;
        background-color: #2ad65e; /* Header background color */
        padding: 20px;
        color: white;
      }
      .title {
        color: black; /* Text color for the title */
        font-size: 30px;
        font-weight: bold;
      }

      .greeting {
        font-size: 18px;
        margin: 10 5;
      }
      .emailBody {
        margin: 5 5;
      }
      .support {
        font-size: 15px;
        font-style: italic;
        margin: 5 5;
      }

      .mainBody {
        background-color: #ffffff; /* Main content background color */
        padding: 20px;
        /* border-radius: 5px; */
        /* box-shadow: 0px 0px 5px 2px #ccc; Add a shadow to the content */
      }
      .body-content {
        /* display: flex;
        flex-direction: column; */
        border: 1px #fff8ea;
        border-radius: 5px;
        margin: 10 5;
        padding: 10px;
        /* background-color: #fff8ea; */
        box-shadow: 0px 0px 5px 2px #ccc;
      }
      .title-content {
        font-weight: bold;
      }

      u i {
        color: blue;
      }

      .footer {
        font-size: 14px;
        text-align: center;
        background-color: #ffba00; /* Header background color */
        padding: 10px;
        display: flex;
        justify-content: center;
        flex-direction: column;
      }
      .footer-text {
        font-weight: 600;
      }
      .signature {
        text-align: right;
        font-size: 16px;
        margin: 5 5;
      }
    </style>
  </head>
  <body>
    <div class=""container"">
      <div
        style=""
          height: 100px;
          display: flex;
          align-items: center;
          justify-content: center;
          background-color: white;
        ""
      >
        <p
          style=""
            color: #515151;
            text-align: center;
            margin: auto 0;
            font-size: 30px;
          ""
        >
          Cóc Event
        </p>
      </div>
      <div class=""mainBody"">
        <!-- <div class=""header-title"">
        </div> -->
        <h2 class=""emailBody"">Hello " + name + @" ,</h2>
        <p class=""greeting""></p>

        <p class=""emailBody"">
         You have accidentally forgotten your password through <b><i>Cóc Event </i></b>.
        </p>
        <p class=""emailBody"">
          Below is your OTP information:
          <b><i> " + body + @"</i></b>
        </p>

        <p class=""emailBody"">
          Please enter the code above into the system to proceed to the next step
          <a href=""https://lovehouse.vercel.app/""
            ><span style=""font-weight: bold; text-transform: uppercase""
              >here</span
            ></a
          >
        </p>
        <p class=""support"">
          Thank you for your interest in the services of <b><i>Cóc Event</i></b
          >, for any inquiries, please contact
          <u><i>qk.backend@gmail.com</i></u> for support
        </p>
        <div class=""signature"">
          <p>Best regards,</p>
          <p>
            <b><i>Cóc Event Team</i></b>
          </p>
        </div>
      </div>
      <div style=""height: 100px"">

      </div>
    </div>
  </body>
</html>

";
                    break;

                case ContentEmailType.SPONSOR_ACCOUNT_CREATION:
                    content = @"
<html>
  <head>
    <style>
      * {
        margin: 0;
        padding: 0;
      }

      body {
        font-family: Arial, sans-serif;
        background-color: #f4f4f4; /* Background color for the entire email */
      }

      .container {
        max-width: 900px;
        margin: 20 auto;
        /* padding: 20px; */
        border-radius: 5px;
        box-shadow: 0px 0px 5px 2px #ccc; /*Add a shadow to the content */
      }

      .header {
        text-align: center;
        background-color: #ffba00; /* Header background color */
        padding: 20px;
      }
      .header-title {
        text-align: left;
        background-color: #2ad65e; /* Header background color */
        padding: 20px;
        color: white;
      }
      .title {
        color: black; /* Text color for the title */
        font-size: 30px;
        font-weight: bold;
      }

      .greeting {
        font-size: 18px;
        margin: 10 5;
      }
      .emailBody {
        margin: 5 5;
      }
      .support {
        font-size: 15px;
        font-style: italic;
        margin: 5 5;
      }

      .mainBody {
        background-color: #ffffff; /* Main content background color */
        padding: 20px;
        /* border-radius: 5px; */
        /* box-shadow: 0px 0px 5px 2px #ccc; Add a shadow to the content */
      }
      .body-content {
        /* display: flex;
        flex-direction: column; */
        border: 1px #fff8ea;
        border-radius: 5px;
        margin: 10 5;
        padding: 10px;
        /* background-color: #fff8ea; */
        box-shadow: 0px 0px 5px 2px #ccc;
      }
      .title-content {
        font-weight: bold;
      }

      u i {
        color: blue;
      }

      .footer {
        font-size: 14px;
        text-align: center;
        background-color: #ffba00; /* Header background color */
        padding: 10px;
        display: flex;
        justify-content: center;
        flex-direction: column;
      }
      .footer-text {
        font-weight: 600;
      }
      .signature {
        text-align: right;
        font-size: 16px;
        margin: 5 5;
      }
    </style>
  </head>
  <body>
    <div class=""container"">
      <div
        style=""
          height: 100px;
          display: flex;
          align-items: center;
          justify-content: center;
          background-color: white;
        ""
      >
        <p
          style=""
            color: #515151;
            text-align: center;
            margin: auto 0;
            font-size: 30px;
          ""
        >
          Cóc Event
        </p>
      </div>
      <div class=""mainBody"">
        <!-- <div class=""header-title"">
        </div> -->
        <h2 class=""emailBody"">Xin chào " + name + @" ,</h2>
        <p class=""greeting""></p>

        <p class=""emailBody"">
         Tài khoản hướng dẫn viên của bạn đã được tọ thành công <b><i>Cóc Event </i></b>.
        </p>
        <p class=""emailBody"">
          Đây là thông tin tài khoản của bạn, hãy thay đỗi mật khẩu
          <b><i> " + body + @"</i></b>
        </p>

        <p class=""emailBody"">
          Please enter the code above into the system to proceed to the next step
          <a href=""https://lovehouse.vercel.app/""
            ><span style=""font-weight: bold; text-transform: uppercase""
              >here</span
            ></a
          >
        </p>
        <p class=""support"">
          Thank you for your interest in the services of <b><i>Cóc Event</i></b
          >, for any inquiries, please contact
          <u><i>qk.backend@gmail.com</i></u> for support
        </p>
        <div class=""signature"">
          <p>Best regards,</p>
          <p>
            <b><i>Cóc Event Team</i></b>
          </p>
        </div>
      </div>
      <div style=""height: 100px"">

      </div>
    </div>
  </body>
</html>

";
                    break;
            }

            return content;
        }

        public static string GetTemplateNotification(string name)
        {
            string body = @"
<html>
  <head>
    <style>
      * {
        margin: 0;
        padding: 0;
      }

      body {
        font-family: Arial, sans-serif;
        background-color: #f4f4f4; /* Background color for the entire email */
      }

      .container {
        max-width: 900px;
        margin: 20 auto;
        /* padding: 20px; */
        border-radius: 5px;
        box-shadow: 0px 0px 5px 2px #ccc; /*Add a shadow to the content */
      }

      .header {
        text-align: center;
        background-color: #ffba00; /* Header background color */
        padding: 20px;
      }
      .header-title {
        text-align: left;
        background-color: #2ad65e; /* Header background color */
        padding: 20px;
        color: white;
      }
      .title {
        color: black; /* Text color for the title */
        font-size: 30px;
        font-weight: bold;
      }

      .greeting {
        font-size: 18px;
        margin: 10 5;
      }
      .emailBody {
        margin: 5 5;
      }
      .support {
        font-size: 15px;
        font-style: italic;
        margin: 5 5;
      }

      .mainBody {
        background-color: #ffffff; /* Main content background color */
        padding: 20px;
        /* border-radius: 5px; */
        /* box-shadow: 0px 0px 5px 2px #ccc; Add a shadow to the content */
      }
      .body-content {
        /* display: flex;
        flex-direction: column; */
        border: 1px #fff8ea;
        border-radius: 5px;
        margin: 10 5;
        padding: 10px;
        /* background-color: #fff8ea; */
        box-shadow: 0px 0px 5px 2px #ccc;
      }
      .title-content {
        font-weight: bold;
      }

      u i {
        color: blue;
      }

      .footer {
        font-size: 14px;
        text-align: center;
        background-color: #ffba00; /* Header background color */
        padding: 10px;
        display: flex;
        justify-content: center;
        flex-direction: column;
      }
      .footer-text {
        font-weight: 600;
      }
      .signature {
        text-align: right;
        font-size: 16px;
        margin: 5 5;
      }
    </style>
  </head>
  <body>
    <div class=""container"">
      <div
        style=""
          height: 100px;
          display: flex;
          align-items: center;
          justify-content: center;
          background-color: white;
        ""
      >
        <p
          style=""
            color: #515151;
            text-align: center;
            margin: auto 0;
            font-size: 30px;
          ""
        >
          Cóc Event
        </p>
      </div>
      <div class=""mainBody"">
        <!-- <div class=""header-title"">
        </div> -->
        <h2 class=""emailBody"">Hello " + name + @" ,</h2>
        <p class=""greeting""></p>

        <p class=""emailBody"">
         Your quote request has been completed at <b><i>Cóc Event </i></b>.
        </p>
        <p class=""emailBody"">
         Please enter the system to view the quote and moderate this quote
        </p>
            <a href=""https://lovehouse.vercel.app/""
            ><span style=""font-weight: bold; text-transform: uppercase""
              >here</span
            ></a
          >
        <p class=""support"">
          Thank you for your interest in the services of <b><i>Cóc Event</i></b
          >, for any inquiries, please contact
          <u><i>qk.backend@gmail.com</i></u> for support
        </p>
        <div class=""signature"">
          <p>Best regards,</p>
          <p>
            <b><i>Cóc Event Team</i></b>
          </p>
        </div>
      </div>
      <div style=""height: 100px"">

      </div>
    </div>
  </body>
</html>

";
            return body;
        }

        public static string GenerateTicketEmailBody(Account account, Dictionary<string, List<IFormFile>> ticketInfo, Event eventInfo)
        {
            var emailTemplate = @"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            color: #333;
            margin: 0;
            padding: 0;
        }
        .container {
            width: 80%;
            margin: 0 auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .header {
            text-align: center;
            padding: 20px 0;
        }
        .header h1 {
            margin: 0;
            color: #007BFF;
        }
        .account-info, .event-info {
            margin: 20px 0;
            padding: 10px;
            background-color: #f9f9f9;
            border-radius: 5px;
        }
        .account-info p, .event-info p {
            margin: 5px 0;
        }
        .ticket-section {
            margin: 20px 0;
        }
        .ticket-section h2 {
            margin: 0 0 10px 0;
            color: #007BFF;
        }
        .ticket-list {
            list-style-type: none;
            padding: 0;
        }
        .ticket-item {
            background-color: #f1f1f1;
            margin: 5px 0;
            padding: 10px;
            border-radius: 5px;
            display: flex;
            align-items: center;
        }
        .ticket-item .icon {
            margin-right: 10px;
        }
        .ticket-item img {
            max-width: 100%;
            height: auto;
            display: block;
        }
        .ticket-item .details {
            display: flex;
            flex-direction: column;
        }
        .ticket-item .file-name,
        .ticket-item .file-size {
            margin: 2px 0;
        }
        .image-grid {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            margin-bottom: 30px;
        }
        .image-grid .ticket-item {
            flex: 0 0 calc(25% - 20px);
            margin-bottom: 30px;
            position: relative;
            list-style-type: none;
            padding: 0;
            text-align: center;
        }
        .image-grid .ticket-item .image-container {
            position: relative;
            overflow: hidden;
            box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
            width: 90%;
            height: 90%;
            padding: 15px;
        }
        .image-grid .ticket-item img {
            width: 100%;
            height: 100%;
            object-fit: cover; /* Maintain aspect ratio */
            transition: transform 0.3s ease;
        }
        .image-grid .ticket-item:hover img {
            transform: scale(1.1);
        }
    </style>
    <script>
        function setDownloadFilename(url, filename) {
            var link = document.createElement('a');
            link.href = url;
            link.download = filename;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
    </script>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>THÔNG TIN VÉ</h1>
        </div>
        <div class='account-info'>
            <h2>THÔNG TIN KHÁCH HÀNG</h2>
            <p><strong>First Name:</strong> {{FirstName}}</p>
            <p><strong>Last Name:</strong> {{LastName}}</p>
            <p><strong>Phone Number:</strong> {{PhoneNumber}}</p>
        </div>
        <div class='event-info'>
            <h2>THÔNG TIN SỰ KIỆN</h2>
            <p><strong>Title:</strong> {{EventTitle}}</p>
            <p><strong>Description:</strong> {{EventDescription}}</p>
            <p><strong>Start Date:</strong> {{EventStartDate}}</p>
            <p><strong>End Date:</strong> {{EventEndDate}}</p>
            <p><strong>Location:</strong> {{EventLocation}}</p>
            <p><strong>Organization:</strong> {{EventOrganization}}</p>
        </div>
        <div class='ticket-section'>
            <h2>Tickets</h2>
           <p>Below are the tickets you have purchased for the event</p>
        </div>
    </div>

    <script>
        document.addEventListener('DOMContentLoaded', function() {
            var ticketLinks = document.querySelectorAll('.ticket-item a');
            ticketLinks.forEach(function(link) {
                link.addEventListener('click', function(event) {
                    event.preventDefault();
                    var url = this.getAttribute('href');
                    var filename = this.getAttribute('data-download-filename');
                    setDownloadFilename(url, filename);
                });
            });
        });
    </script>
</body>
</html>
";


            var emailBody = emailTemplate
                .Replace("{{FirstName}}", account.FirstName)
                .Replace("{{LastName}}", account.LastName)
                .Replace("{{PhoneNumber}}", account.PhoneNumber)
                .Replace("{{EventTitle}}", eventInfo.Title)
                .Replace("{{EventDescription}}", eventInfo.Description)
                .Replace("{{EventStartDate}}", eventInfo.StartEventDate.ToString("yyyy-MM-dd HH:mm"))
                .Replace("{{EventEndDate}}", eventInfo.EndEventDate.ToString("yyyy-MM-dd HH:mm"))
                .Replace("{{EventLocation}}", eventInfo.Location != null ? eventInfo.Location.Name : "N/A")
                .Replace("{{EventOrganization}}", eventInfo.Organization != null ? eventInfo.Organization.Name : "N/A");

            return emailBody;
        }

        private static string GetFileNameFromUrl(string url)
        {
            return Path.GetFileName(url);
        }

        public static string GetPolicy()
        {
            StringBuilder policy = new StringBuilder();

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");


            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            // Giới thiệu
            policy.AppendLine("1. Giới thiệu");
            policy.AppendLine("1.1. Chính sách này nhằm bảo vệ dữ liệu.");
            policy.AppendLine("1.2. Bảo vệ thông tin người dùng là ưu tiên.");
            policy.AppendLine("1.3. Chính sách này áp dụng cho tất cả nhân viên.");
            policy.AppendLine("1.4. Chính sách này cũng áp dụng cho tất cả nhà cung cấp.");
            policy.AppendLine("1.5. Người dùng bao gồm khách hàng và nhân viên.");
            policy.AppendLine("1.6. Chúng tôi cam kết bảo vệ quyền riêng tư.");
            policy.AppendLine("1.7. Dữ liệu cá nhân là thông tin nhận dạng.");
            policy.AppendLine("1.8. Bảo vệ dữ liệu là trách nhiệm của chúng tôi.");
            policy.AppendLine("1.9. Chúng tôi tuân thủ luật pháp về bảo mật.");
            policy.AppendLine("1.10. Chính sách này được cập nhật định kỳ.");

            // Thu thập thông tin
            policy.AppendLine("2. Thu thập thông tin");
            policy.AppendLine("2.1. Chúng tôi thu thập thông tin cần thiết.");
            policy.AppendLine("2.2. Thông tin thu thập bao gồm tên, email.");
            policy.AppendLine("2.3. Chúng tôi cũng thu thập số điện thoại.");
            policy.AppendLine("2.4. Địa chỉ nhà và công việc cũng được thu thập.");
            policy.AppendLine("2.5. Thông tin tài khoản ngân hàng được bảo mật.");
            policy.AppendLine("2.6. Chúng tôi thu thập dữ liệu qua các biểu mẫu.");
            policy.AppendLine("2.7. Dữ liệu cũng được thu thập qua trang web.");
            policy.AppendLine("2.8. Chúng tôi sử dụng cookie để thu thập dữ liệu.");
            policy.AppendLine("2.9. Dữ liệu được lưu trữ an toàn.");
            policy.AppendLine("2.10. Chúng tôi không thu thập thông tin nhạy cảm.");

            // Sử dụng thông tin
            policy.AppendLine("3. Sử dụng thông tin");
            policy.AppendLine("3.1. Thông tin được sử dụng để cung cấp dịch vụ.");
            policy.AppendLine("3.2. Thông tin cũng được sử dụng để cải thiện dịch vụ.");
            policy.AppendLine("3.3. Chúng tôi sử dụng thông tin để liên hệ khách hàng.");
            policy.AppendLine("3.4. Thông tin được sử dụng cho mục đích tiếp thị.");
            policy.AppendLine("3.5. Chúng tôi không bán thông tin cá nhân.");
            policy.AppendLine("3.6. Thông tin được sử dụng theo luật pháp.");
            policy.AppendLine("3.7. Chúng tôi sử dụng thông tin để bảo vệ quyền lợi.");
            policy.AppendLine("3.8. Thông tin được sử dụng để phòng chống gian lận.");
            policy.AppendLine("3.9. Chúng tôi sử dụng thông tin để nghiên cứu.");
            policy.AppendLine("3.10. Thông tin được sử dụng để phân tích.");

            // Bảo mật thông tin
            policy.AppendLine("4. Bảo mật thông tin");
            policy.AppendLine("4.1. Chúng tôi áp dụng các biện pháp bảo mật mạnh mẽ.");
            policy.AppendLine("4.2. Thông tin được mã hóa để bảo mật.");
            policy.AppendLine("4.3. Chúng tôi kiểm soát truy cập thông tin.");
            policy.AppendLine("4.4. Chỉ nhân viên được phép mới truy cập thông tin.");
            policy.AppendLine("4.5. Thông tin được lưu trữ trong môi trường an toàn.");
            policy.AppendLine("4.6. Chúng tôi sử dụng tường lửa để bảo vệ hệ thống.");
            policy.AppendLine("4.7. Thông tin sao lưu được bảo mật.");
            policy.AppendLine("4.8. Chúng tôi kiểm tra bảo mật định kỳ.");
            policy.AppendLine("4.9. Thông tin được bảo vệ khỏi truy cập trái phép.");
            policy.AppendLine("4.10. Chúng tôi áp dụng các biện pháp phát hiện xâm nhập.");

            // Quyền của người dùng
            policy.AppendLine("5. Quyền của người dùng");
            policy.AppendLine("5.1. Người dùng có quyền truy cập thông tin cá nhân.");
            policy.AppendLine("5.2. Người dùng có quyền yêu cầu chỉnh sửa thông tin.");
            policy.AppendLine("5.3. Người dùng có quyền yêu cầu xóa thông tin.");
            policy.AppendLine("5.4. Người dùng có quyền hạn chế sử dụng thông tin.");
            policy.AppendLine("5.5. Người dùng có quyền phản đối việc sử dụng thông tin.");
            policy.AppendLine("5.6. Người dùng có quyền khiếu nại về bảo mật.");
            policy.AppendLine("5.7. Chúng tôi cam kết đáp ứng yêu cầu người dùng.");
            policy.AppendLine("5.8. Người dùng có quyền rút lại sự đồng ý.");
            policy.AppendLine("5.9. Chúng tôi cung cấp thông tin rõ ràng về quyền.");
            policy.AppendLine("5.10. Người dùng có thể liên hệ để thực hiện quyền.");

            // Chia sẻ thông tin
            policy.AppendLine("6. Chia sẻ thông tin");
            policy.AppendLine("6.1. Chúng tôi chỉ chia sẻ thông tin khi cần thiết.");
            policy.AppendLine("6.2. Thông tin được chia sẻ với nhà cung cấp dịch vụ.");
            policy.AppendLine("6.3. Nhà cung cấp dịch vụ phải tuân thủ chính sách này.");
            policy.AppendLine("6.4. Thông tin được chia sẻ với cơ quan pháp luật.");
            policy.AppendLine("6.5. Chúng tôi không chia sẻ thông tin với bên thứ ba.");
            policy.AppendLine("6.6. Thông tin chỉ được chia sẻ khi có sự đồng ý.");
            policy.AppendLine("6.7. Chúng tôi thông báo cho người dùng khi chia sẻ thông tin.");
            policy.AppendLine("6.8. Thông tin chia sẻ được bảo vệ.");
            policy.AppendLine("6.9. Chúng tôi kiểm soát việc chia sẻ thông tin.");
            policy.AppendLine("6.10. Chúng tôi không chia sẻ thông tin nhạy cảm.");

            // Trách nhiệm và tuân thủ
            policy.AppendLine("7. Trách nhiệm và tuân thủ");
            policy.AppendLine("7.1. Nhân viên phải tuân thủ chính sách bảo mật.");
            policy.AppendLine("7.2. Nhân viên được đào tạo về bảo mật thông tin.");
            policy.AppendLine("7.3. Chúng tôi kiểm tra tuân thủ định kỳ.");
            policy.AppendLine("7.4. Nhân viên vi phạm sẽ bị xử lý nghiêm khắc.");
            policy.AppendLine("7.5. Chúng tôi có trách nhiệm bảo vệ thông tin.");
            policy.AppendLine("7.6. Chúng tôi báo cáo vi phạm bảo mật kịp thời.");
            policy.AppendLine("7.7. Chúng tôi tuân thủ luật pháp về bảo mật thông tin.");
            policy.AppendLine("7.8. Chúng tôi hợp tác với cơ quan pháp luật.");
            policy.AppendLine("7.9. Chính sách này được giám sát và kiểm tra.");
            policy.AppendLine("7.10. Chúng tôi cam kết bảo vệ quyền riêng tư.");

            // Thay đổi chính sách
            policy.AppendLine("8. Thay đổi chính sách");
            policy.AppendLine("8.1. Chính sách này có thể được cập nhật.");
            policy.AppendLine("8.2. Chúng tôi thông báo người dùng về thay đổi.");
            policy.AppendLine("8.3. Thay đổi sẽ có hiệu lực sau khi thông báo.");
            policy.AppendLine("8.4. Người dùng có thể từ chối thay đổi chính sách.");
            policy.AppendLine("8.5. Chúng tôi khuyến khích người dùng kiểm tra chính sách.");
            policy.AppendLine("8.6. Chính sách mới sẽ được đăng tải trên trang web.");
            policy.AppendLine("8.7. Chúng tôi cam kết bảo mật thông tin ngay cả khi thay đổi.");
            policy.AppendLine("8.8. Mọi thay đổi sẽ tuân thủ luật pháp.");
            policy.AppendLine("8.9. Thay đổi sẽ không làm giảm quyền của người dùng.");
            policy.AppendLine("8.10. Chính sách cũ sẽ được lưu trữ để tham khảo.");

            return policy.ToString();
        }


    }
}