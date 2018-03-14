<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Calculo_Comisiones_Operadores._Default" %>

<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml" lang="en" class="no-js">
<head runat="server">
    <link href='https://fonts.googleapis.com/css?family=Pinyon+Script' rel='stylesheet' type='text/css' />
    <%--<link rel="stylesheet" href="alertifyjs/css/themes/bootstrap.css" />--%>

    <style type="text/css">
        * {
            margin: 0px;
            padding: 0px;
        }

        body {
            background: url(imagenes/blackG.jpg) #2b2b2a;
        }

        form {
            background: url(imagenes/blueG.jpg) #192ec6;
            width: 360px;
            border: 1px solid #4e4d4d;
            border-radius: 3px;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            box-shadow: inset 0 0 10px #000;
            margin: 100px auto;
        }

            form h4 {
                text-aling: center;
                color: #FFFFFF;
                font-weight: normal;
                font-size: 40pt;
                margin: 30px 0px;
                font-family: 'Pinyon Script', cursive;
            }

            form label {
                color: #000;
                font-weight: normal;
                font-size: 12pt;
                float: left;
                font-family: Vegur, 'PT Sans', Verdana, Sans-serif;
            }

            form input {
                width: 280px;
                height: 35px;
                padding: 0px 10px;
                color: #6d6d6d;
                text-aling: center;
            }

            form button {
                width: 135px;
                margin: 20px 0px 30px 30px;
                height: 50px;
            }

        #logo {
            height: 99px;
            width: 322px;
        }
    </style>
</head>

<body leftmargin="0" topmargin="0" marginwidth="0" bgcolor="#E6E6E6">


    <div id="contenedor">
        <br />
        <br />
        <br />
        <br />
        <div id="caja_login">
            <br />
            <center>
					
					<form id="Login" method="post" runat="server">
					<!--<h4>Iniciar Sesion</h4>-->
                    <br />
                    <img id="logo" alt="Brand" src="imagenes/login.png" class=""/>
                    <br /><br />
                    <label>&nbsp<b> Usuario: </b></label><br/>
					<asp:TextBox ID="txtUsername" Runat="server" placeholder="root" width="80%"></asp:TextBox><br/><br/>

                    <label>&nbsp<b> Password: </b></label><br/>
					<asp:TextBox ID="txtPassword" Runat="server" TextMode="Password" placeholder="*****" width="80%"></asp:TextBox><br/><br/>
					<br/>

					<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="ACEPTAR" />
                    <br/><br/>
					<asp:Label ID="errorLabel" Runat="server" ForeColor="#ff3300"><b></b></asp:Label><br/>
						

					</form>
					</center>
        </div>
    </div>

</body>
</html>

