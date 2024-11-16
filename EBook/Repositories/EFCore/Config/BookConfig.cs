using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Models;

namespace Repositories.EFCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        private Book CreateBook(int id, string title)
        {
            return new Book { Id = id, Title = title };
        }

        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                CreateBook(1, "İtaatsizlik Üzerine"),
                CreateBook(2, "Sevme Sanatı"),
                CreateBook(3, "Yas ve Melankoli"),
                CreateBook(4, "Ego ve Id"),
                CreateBook(5, "Totem ve Tabu"),
                CreateBook(6, "Uygarlığın Huzursuzluğu"),
                CreateBook(7, "Dört Arketip"),
                CreateBook(8, "Yaşama Sanatı"),
                CreateBook(9, "Kitle Psikolojisi"),
                CreateBook(10, "Rüyaların Yorumu")
            );
        }      
    }
}
