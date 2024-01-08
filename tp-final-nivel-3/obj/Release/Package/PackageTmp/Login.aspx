﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tp_final_nivel_3.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
            .validacion{
                color: red;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-4">
            <h2>Login</h2>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" cssclass="form-control" ID="txtEmail"/>
                <asp:RequiredFieldValidator CssClass="validacion" ErrorMessage="Por favor ingrese el Email" ControlToValidate="txtEmail" runat="server" />
                <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Debe ingresar un Email" ControlToValidate="txtEmail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox runat="server" cssclass="form-control" ID="txtPassword" TextMode="Password"/>
                <asp:RequiredFieldValidator CssClass="validacion" ErrorMessage="Por favor ingrese la contraseña" ControlToValidate="txtPassword" runat="server" />
            </div>
            <asp:Button Text="Ingresar" cssclass="btn btn-primary" ID="btnLogin" OnClick="btnLogin_Click" runat="server" />
            <a href="/">Cancelar</a>
        </div>
    </div>
</asp:Content>
