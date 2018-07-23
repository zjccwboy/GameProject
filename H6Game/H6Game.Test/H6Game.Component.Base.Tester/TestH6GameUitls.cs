using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H6Game.Uitls;

namespace H6Game.Component.Base.Tester
{
    [TestClass]
    public class TestH6GameUitls
    {
        [TestMethod]
        public void H6GameUitlsTest()
        {
            var key1 = EncryptUtils.SHA256Encrypt("aaaaaaaaaaaaa");
            var key2= EncryptUtils.SHA256Encrypt("aaaaaaaaaaaaa");
            Assert.AreEqual(key1, key2);

            key1 = EncryptUtils.SHA256Encrypt("afafafasfasfasfashd34562135ui6578sdgsdgsgfwerhd34634");
            key2 = EncryptUtils.SHA256Encrypt("afafafasfasfasfashd34562135ui6578sdgsdgsgfwerhd34634");
            Assert.AreEqual(key1, key2);

            key1 = EncryptUtils.SHA256Encrypt("5465786798afaoijrqwaoiurqiawjroqiawrhqiwhrqwqioadjioadoia");
            key2 = EncryptUtils.SHA256Encrypt("5465786798afaoijrqwaoiurqiawjroqiawrhqiwhrqwqioadjioadoia");
            Assert.AreEqual(key1, key2);
        }
    }
}
