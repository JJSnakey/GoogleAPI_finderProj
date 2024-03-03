<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TryItPageAspNET.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 505px">
    <form id="form1" runat="server">
        Try It Page<p>
            --Required Services--</p>
        <p>
            -Crime Data Service:</p>
        <p>
            City:&nbsp;
            <asp:TextBox ID="TextBox3" runat="server" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
&nbsp;(Enter a valid city name within the state, capitalize the first letter after each space)</p>
        <p>
            State:&nbsp;
            <asp:TextBox ID="TextBox4" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
            (Enter a valid state abbreviation, 2 capital letters)</p>
        <p>
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Crimes Per Year" Width="140px" />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label5" runat="server" Text="Results:"></asp:Label>
        </p>
        <p>
            -Natural Hazards Service:</p>
        <p>
            Latitude:<asp:TextBox ID="TextBox5" runat="server" OnTextChanged="TextBox5_TextChanged"></asp:TextBox>
            (Enter a valid latitude value)</p>
        <p>
            Longitude:<asp:TextBox ID="TextBox6" runat="server" OnTextChanged="TextBox6_TextChanged"></asp:TextBox>
            (Enter a valid latitude value)</p>
        <p>
            Radius:<asp:TextBox ID="TextBox7" runat="server" OnTextChanged="TextBox7_TextChanged"></asp:TextBox>
            (Enter a positive integer for Radius in Kilometers, recommended 1000)</p>
        <p>
            Minimum Magnitude:<asp:TextBox ID="TextBox8" runat="server" OnTextChanged="TextBox8_TextChanged"></asp:TextBox>
            (Enter a postive value with up to 1 decimal, recommended 3.0)</p>
        <p>
            <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Earthquakes Per Year" Width="172px" />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label6" runat="server" Text="Results:"></asp:Label>
        </p>
        <p>
            --Elective Services-- </p>
        <p>
            (Latitude and Longitde are inputs for the following services)</p>
        <p>
            Latitude:<asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            (Enter a valid latitude value)</p>
        <p>
            Longitude:<asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
            (Enter a valid latitude value)</p>
        <p>
            -Parks Service:</p>
        <p>
&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Find Park" Width="123px" />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="Results:"></asp:Label>
        </p>
        <p>
            -Movie Theaters Service:</p>
        <p>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Find Theater" Width="128px" />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Results:"></asp:Label>
        </p>
        <p>
            -Schools Service (RESTful):</p>
        <p>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Find School" />
&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Results:"></asp:Label>
        </p>
        <p style="width: 269px">
            -Bus Stops Service:</p>
        <p style="width: 1047px">
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Find Bus Station" Width="127px" />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Text="Results:"></asp:Label>
        </p>
    </form>
</body>
</html>
