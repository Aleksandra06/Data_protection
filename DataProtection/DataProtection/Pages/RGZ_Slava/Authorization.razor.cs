using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Math;

namespace DataProtection.Pages.RGZ_Slava
{
    public partial class Authorization
    {
        [Inject] public Server server { get; set; }
        /*
         * Varibale for Output to page
         */
        public BigInteger x;
        public BigInteger y;
        public BigInteger v;
        public int e;

        /*
         * Varibale basic output to page
         */
        protected BigInteger output;
        protected List<string> outputString = new List<string>();
        protected string outputMessage;
        protected string outputStatus;
        protected List<string> outputCheckLeft = new List<string>();
        protected List<string> outputCheckRight = new List<string>();

        /*
         * Varibale login and password
         */
        protected string login;
        protected string password;
        /*
         * Varibale for page
         */
        protected int choose = 0;
        protected bool tableShow = false; 

        public void regist()
        {
            Server server = new Server();
            Client client = new Client();

            if (!string.IsNullOrEmpty(login)) {
                client.saveLogin(login);
                if (client.checkNaboutServer(server)) {
                    if (server.checkLogin(client.getLogin())) {
                        outputStatus = "Вы есть в нашей базе, попробуйте Авторизоваться";
                        
                    } else {
                        outputStatus = "Вас нет в нашей базе, генерируем вам пароль";
                        tableShow = false;
                        outputMessage = "";
                        register(client, server);
                    }
                } else {
                    outputStatus = "Сменился сервер, генерируем вам пароль";
                    tableShow = false;
                    outputMessage = "";
                    registerReload(client, server);
                }
                outputMessage = "";
            }
        }
        public void auth()
        {
            //Server server = new Server();
            Client client = new Client();

            if (!string.IsNullOrEmpty(login)) {
                client.saveLogin(login);
                if (client.checkNaboutServer(server)) {
                    if (server.checkLogin(client.getLogin())) {
                        outputStatus = "Вы есть в нашей базе";
                        identify(client, server);
                    } else {
                        outputStatus = "Вас нет в нашей базе, попробуйте Зарегистрироваться";
                        tableShow = false;
                        outputMessage = "";
                    }
                } else {
                    outputStatus = "Сменился сервер, генерируем вам пароль";
                    tableShow = false;
                    outputMessage = "";
                    registerReload(client, server);
                }
            }

        }
        public void register(Client client, Server server)
        {
            client.connect(server.getN());
            client.generateSV();
            server.connect(client.getV());
            server.savePublicData();
            client.savePublicData();

            outputString = new List<string>();
            server.setT(40);
            for (int i = 0; i < server.getT(); i++) {
                server.responseX(client.sendX());
                client.responseE(server.sendE());
                if (server.responseY(client.sendY())) {
                    outputString.Add("Успешно");
                    outputCheckLeft.Add(server.getLeft());
                    outputCheckRight.Add(server.getRight());
                } else {
                    outputString.Add("Ошибка");
                    outputCheckLeft.Add(server.getLeft());
                    outputCheckRight.Add(server.getRight());
                    break;
                }
            }

            if (outputString.Find(x => x == "Ошибка") == "Ошибка") {
                outputMessage = "Регистрация не корректна";
            } else {
                outputMessage = "Регистрация пройдена";
                client.savePassword();
                server.saveClient(client.getLogin(), client.getV());
                tableShow = true;
            }
        }
        public void identify(Client client, Server server)
        {
            outputString = new List<string>();

            client.generateV(password);
            if (server.connect(client.getLogin(), client.getV())) {
                server.setT(40);
                for (int i = 0; i < server.getT(); i++) {
                    server.responseX(client.sendX());
                    client.responseE(server.sendE());
                    if (server.responseY(client.sendY())) {
                        outputString.Add("Успешно");
                        outputCheckLeft.Add(server.getLeft());
                        outputCheckRight.Add(server.getRight());
                    } else {
                        outputString.Add("Ошибка");
                        outputCheckLeft.Add(server.getLeft());
                        outputCheckRight.Add(server.getRight());
                        break;
                    }
                    
                }

                if (outputString.Find(x => x == "Ошибка") == "Ошибка") {
                    outputMessage = "Авторизация не корректна";
                } else {
                    outputMessage = "Авторизация пройдена";
                    tableShow = true;
                }
            } else {
                outputMessage = "Не верный Логин или Пароль";
            }
        }
        public void registerReload(Client client, Server server)
        {
            client.connect(server.getN());
            client.generateSV();
            server.connect(client.getV());
            server.savePublicData();
            client.savePublicData();

            outputString = new List<string>();
            server.setT(40);
            for (int i = 0; i < server.getT(); i++) {
                server.responseX(client.sendX());
                client.responseE(server.sendE());
                if (server.responseY(client.sendY())) {
                    outputString.Add("Успешно");
                } else {
                    outputString.Add("Ошибка");
                    break;
                }
            }

            if (outputString.Find(x => x == "Ошибка") == "Ошибка") {
                outputMessage = "Регистрация не корректна";
            } else {
                outputMessage = "Регистрация пройдена";
                client.deletePassword();
                client.savePassword();
                server.deleteClient(client.getLogin());
                server.saveClient(client.getLogin(), client.getV());
                tableShow = true;
            }
        }

        //protected string BaseUrl { get; set; } = "Helper/GetPdfBearbeitenPageByTemplateId?templateId=";
        //protected string UrlData { get; set; }
        //protected void UpdatePdfData()
        //{
        //    Guid guid = Guid.NewGuid();
        //    UrlData = BaseUrl + "&guid=" + guid;
        //}

    }
}
