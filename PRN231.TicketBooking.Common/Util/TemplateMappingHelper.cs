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
          Love House
        </p>
      </div>
      <div class=""mainBody"">
        <!-- <div class=""header-title"">
        </div> -->
        <h2 class=""emailBody"">Hello " + name + @" ,</h2>
        <p class=""greeting""></p>

        <p class=""emailBody"">
          You are currently registering an account through <b><i>Love House </i></b>.
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
          Thank you for your interest in the services of <b><i>Love House</i></b
          >, for any inquiries, please contact
          <u><i>qk.backend@gmail.com</i></u> for support
        </p>
        <div class=""signature"">
          <p>Best regards,</p>
          <p>
            <b><i>Love House Team</i></b>
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
          Love House
        </p>
      </div>
      <div class=""mainBody"">
        <!-- <div class=""header-title"">
        </div> -->
        <h2 class=""emailBody"">Hello " + name + @" ,</h2>
        <p class=""greeting""></p>

        <p class=""emailBody"">
          You are in the process of completing contract procedures through <b><i>Love House </i></b>.
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
          Thank you for your interest in the services of <b><i>Love House</i></b
          >, for any inquiries, please contact
          <u><i>qk.backend@gmail.com</i></u> for support
        </p>
        <div class=""signature"">
          <p>Best regards,</p>
          <p>
            <b><i>Love House Team</i></b>
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
          Love House
        </p>
      </div>
      <div class=""mainBody"">
        <!-- <div class=""header-title"">
        </div> -->
        <h2 class=""emailBody"">Hello " + name + @" ,</h2>
        <p class=""greeting""></p>

        <p class=""emailBody"">
         You have accidentally forgotten your password through <b><i>Love House </i></b>.
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
          Thank you for your interest in the services of <b><i>Love House</i></b
          >, for any inquiries, please contact
          <u><i>qk.backend@gmail.com</i></u> for support
        </p>
        <div class=""signature"">
          <p>Best regards,</p>
          <p>
            <b><i>Love House Team</i></b>
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
          Love House
        </p>
      </div>
      <div class=""mainBody"">
        <!-- <div class=""header-title"">
        </div> -->
        <h2 class=""emailBody"">Hello " + name + @" ,</h2>
        <p class=""greeting""></p>

        <p class=""emailBody"">
         Your quote request has been completed at <b><i>Love House </i></b>.
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
          Thank you for your interest in the services of <b><i>Love House</i></b
          >, for any inquiries, please contact
          <u><i>qk.backend@gmail.com</i></u> for support
        </p>
        <div class=""signature"">
          <p>Best regards,</p>
          <p>
            <b><i>Love House Team</i></b>
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

        public static string GenerateTicketEmailBody(Account account, Event eventInfo)
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
            {{TicketSections}}
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




    }
}