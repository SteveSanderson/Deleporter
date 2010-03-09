<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<DateTime>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Index</title>
    </head>
    <body>
        Hello - the current date is 
        <b><span id="time"><%= Model.ToString("h:mm:ss tt") %></span></b>
        on 
        <b><span id="date"><%= Model.ToString("MMMM d, yyyy") %></span></b>.

        <div id="extraInfo">
            <% if (Model < new DateTime(1991, 1, 1)) { %>
                The world wide web hasn't been invented yet - how confusing...
            <% } else { %>
                Nothing special is happening
            <% } %>
        </div>
    </body>
</html>
