import "./ProductCard.css";
// 1. ضيف القلوب
import { FaShoppingCart, FaHeart, FaRegHeart } from "react-icons/fa";
// 2. استدعي الكونتكست (لو عامل الكارت) واستدعي المفضلة
// import { useCart } from "./CartContext.jsx";
import { useFavorites } from "./FavoritesContext.jsx";

// 3. غيّر الـ props هنا عشان تستقبل "product" بس
function ProductCard({ product }) {
  // const { addToCart } = useCart();
  // 4. اسحب الفانكشنز بتاعة المفضلة
  const { toggleFavorite, isFavorite } = useFavorites();

  // 5. شوف المنتج ده في المفضلة ولا لأ
  const isItemFavorite = isFavorite(product.id);

  const handleAddToCart = () => {
    // addToCart(product);
    alert(`${product.title} تم إضافته للعربة!`);
  };

  const handleToggleFavorite = () => {
    toggleFavorite(product);
  };

  return (
    <div className="product-card">
      {/* 6. ضيف أيقونة القلب هنا */}
      <button className="favorite-btn" onClick={handleToggleFavorite}>
        {isItemFavorite ? <FaHeart color="red" /> : <FaRegHeart />}
      </button>

      {/* 7. استخدم الداتا من الأوبجكت الجديد */}
      <div className="product-badge">{product.discountPercentage}% خصم</div>
      <div className="product-image-container">
        <img
          src={product.imgSrc}
          alt={product.title}
          className="product-image"
        />
      </div>
      <div className="product-info">
        <h3 className="product-title">{product.title}</h3>
        <div className="product-price">
          <span className="new-price">ج.م {product.newPrice}</span>
          <span className="old-price">ج.م {product.oldPrice}</span>
        </div>
        <button className="add-to-cart-btn" onClick={handleAddToCart}>
          <FaShoppingCart /> أضف إلى العربة
        </button>
      </div>
    </div>
  );
}

export default ProductCard;
