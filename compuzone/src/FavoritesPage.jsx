import { useFavorites } from "./FavoritesContext.jsx";
import ProductCard from "./ProductCard.jsx";
import "./FavoritesPage.css";
import { Link } from "react-router-dom";

function FavoritesPage() {
  const { favoritesItems } = useFavorites(); // <-- هات المنتجات من المخزن

  return (
    <div className="favorites-page-container">
      <h1>المفضلة</h1>
      {favoritesItems.length === 0 ? (
        <div className="empty-favorites">
          <p>قائمة المفضلة فارغة.</p>
          <Link to="/" className="favorites-button">
            ابدأ التسوق
          </Link>
        </div>
      ) : (
        <div className="favorites-grid">
          {/* اعرض المنتجات اللي في المفضلة */}
          {favoritesItems.map((item) => (
            <ProductCard key={item.id} product={item} />
          ))}
        </div>
      )}
    </div>
  );
}

export default FavoritesPage;
