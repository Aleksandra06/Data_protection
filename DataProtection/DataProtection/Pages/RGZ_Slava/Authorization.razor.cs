using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;

namespace DataProtection.Pages.RGZ_Slava
{
    public partial class Authorization
    {
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
                        register(client, server);
                    }
                } else {
                    outputStatus = "Сменился сервер, генерируем вам пароль";
                    registerReload(client, server);
                }
            }
        }
        public void auth()
        {
            Server server = new Server();
            Client client = new Client();

            if (!string.IsNullOrEmpty(login)) {
                client.saveLogin(login);
                if (client.checkNaboutServer(server)) {
                    if (server.checkLogin(client.getLogin())) {
                        outputStatus = "Вы есть в нашей базе";
                        identify(client, server);
                    } else {
                        outputStatus = "Вас нет в нашей базе, попробуйте Зарегистрироваться";
                    }
                } else {
                    outputStatus = "Сменился сервер, генерируем вам пароль";
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
                } else {
                    outputString.Add("Ошибка");
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
            client.generateV(password);
            if (server.connect(client.getLogin(), client.getV())) {
                outputString = new List<string>();
                server.setT(40);
                for (int i = 0; i < server.getT(); i++) {
                    server.responseX(client.sendX());
                    client.responseE(server.sendE());
                    if (server.responseY(client.sendY())) {
                        outputString.Add("Успешно");
                    } else {
                        outputString.Add("Ошибка");
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
    }
}
