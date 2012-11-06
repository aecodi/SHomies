using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SHomies.Core.Almacen;

namespace SHomies.Core.Test
{
    [TestFixture]
    public class ProductoTest
    {
        private string cadenaConexion = @"Data Source=OPPDEV08\Shomies;Initial Catalog=SHomies.Tienda;User ID=sa;Password=eltin10al;Pooling=False";
        [Test]
        public void ListaPorCategoria_CASO_1_CUANDO_categoria_igual_a_3_ENTONCES_lista_no_tiene_datos()
        {
            Producto categoria = new Producto(new SHomies.Conexion.SQL.ConexionSQL(cadenaConexion));

            Assert.AreEqual(0, categoria.ListaPorCategoria(new SHomies.Entity.Almacen.Categoria()
            {
                Id = 3
            }).Count);
        }
    }
}
