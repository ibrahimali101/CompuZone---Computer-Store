import "./ProductCard.css";
import { FaShoppingCart } from "react-icons/fa";

function ProductCard({
  imgSrc,
  title,
  oldPrice,
  newPrice,
  discountPercentage,
}) {
  return (
    <div className="product-card">
      <div className="product-badge">{discountPercentage}% خصم</div>
      <div className="product-image-container">
        <img src={imgSrc} alt={title} className="product-image" />
      </div>
      <div className="product-info">
        <h3 className="product-title">{title}</h3>
        <div className="product-price">
          <span className="new-price">ج.م {newPrice}</span>
          <span className="old-price">ج.م {oldPrice}</span>
        </div>
        <button className="add-to-cart-btn">
          <FaShoppingCart /> أضف إلى العربة
        </button>
      </div>
    </div>
  );
}

export default ProductCard;
