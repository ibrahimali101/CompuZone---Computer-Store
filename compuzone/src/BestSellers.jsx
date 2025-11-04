import "./BestSellers.css";
import ProductCard from "./ProductCard.jsx";

const bestSellersData = [
  {
    id: 9,
    imgSrc: "https://picsum.photos/id/1015/400/300",
    title: "iPhone 9",
    oldPrice: "15000",
    newPrice: "14200",
    discountPercentage: 5,
  },
  {
    id: 10,
    imgSrc: "https://picsum.photos/id/1024/400/300",
    title: "موبايل سامسونج S22 Ultra",
    oldPrice: "30000",
    newPrice: "28000",
    discountPercentage: 7,
  },
  {
    id: 11,
    imgSrc: "https://picsum.photos/id/1060/400/300",
    title: "iPhone X",
    oldPrice: "22000",
    newPrice: "20500",
    discountPercentage: 8,
  },
  {
    id: 12,
    imgSrc: "https://picsum.photos/id/1070/400/300",
    title: "Samsung Universe 9",
    oldPrice: "9000",
    newPrice: "8100",
    discountPercentage: 10,
  },
  {
    id: 13,
    imgSrc: "https://picsum.photos/id/1084/400/300",
    title: "ساعة ذكية Huawei Watch GT4",
    oldPrice: "8500",
    newPrice: "7700",
    discountPercentage: 9,
  },
  {
    id: 14,
    imgSrc: "https://picsum.photos/id/1080/400/300",
    title: "تابلت iPad Pro 11 بوصة",
    oldPrice: "32000",
    newPrice: "29500",
    discountPercentage: 8,
  },
];


function BestSellers() {
  return (
    <>
      <section className="bestseller-divider">
        {/* النص هيبقى جوه الـ div ده عادي، مفيش SVG هنا */}
        <div className="bestseller-divider-text">الأكثر مبيعاً</div>
      </section>

      <div className="bestseller-products-container">
        {bestSellersData.map((product) => (
          <ProductCard
            key={product.id}
            imgSrc={product.imgSrc}
            title={product.title}
            oldPrice={product.oldPrice}
            newPrice={product.newPrice}
            discountPercentage={product.discountPercentage}
          />
        ))}
      </div>
    </>
  );
}
export default BestSellers;
