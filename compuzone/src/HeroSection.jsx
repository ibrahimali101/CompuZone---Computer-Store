import "./HeroSection.css";
import ProductCard from "./ProductCard.jsx";

const sampleProducts = [
  {
    id: 1,
    imgSrc: "https://picsum.photos/id/180/400/300",
    title: "لابتوب جيمنج G15 RAM 16GB RTX 4060",
    oldPrice: "35000",
    newPrice: "32500",
    discountPercentage: 7,
  },
  {
    id: 2,
    imgSrc: "https://picsum.photos/id/27/400/300",
    title: "شاشة سامسونج 27 بوصة 144Hz",
    oldPrice: "8000",
    newPrice: "7100",
    discountPercentage: 11,
  },
  {
    id: 3,
    imgSrc: "https://picsum.photos/id/1080/400/300",
    title: "كارت شاشة ZOTAC RTX 4070 Super",
    oldPrice: "25000",
    newPrice: "23000",
    discountPercentage: 8,
  },
  {
    id: 4,
    imgSrc: "https://picsum.photos/id/1062/400/300",
    title: "ماوس لوجيتك G Pro X Superlight",
    oldPrice: "4000",
    newPrice: "3200",
    discountPercentage: 20,
  },
  {
    id: 5,
    imgSrc: "https://picsum.photos/id/1084/400/300",
    title: "كيبورد ميكانيكال RGB",
    oldPrice: "2200",
    newPrice: "1800",
    discountPercentage: 18,
  },
  {
    id: 6,
    imgSrc: "https://picsum.photos/id/1025/400/300",
    title: "سماعة جيمنج HyperX Cloud II",
    oldPrice: "3000",
    newPrice: "2650",
    discountPercentage: 12,
  },
];


function HeroSection() {
  return (
    <>
      <div className="img1">
        <img src="https://miatlantic.com/pub/media/magefan_blog/rtx5090-banner.png" />
      </div>
      <section className="divider">
        <div className="divider-text">افضل العروض</div>
      </section>

      <div className="featured-products-container">
        {sampleProducts.map((product) => (
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
export default HeroSection;
