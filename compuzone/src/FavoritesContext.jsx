import { createContext, useState, useContext } from "react";

const FavoritesContext = createContext();

export function FavoritesProvider({ children }) {
  const [favoritesItems, setFavoritesItems] = useState([]);

  // فنكشن لإضافة أو إزالة منتج
  const toggleFavorite = (product) => {
    setFavoritesItems((prevItems) => {
      const isExisting = prevItems.find((item) => item.id === product.id);

      if (isExisting) {
        // لو موجود، امسحه
        return prevItems.filter((item) => item.id !== product.id);
      } else {
        // لو مش موجود، ضيفه
        return [...prevItems, product];
      }
    });
  };

  // فنكشن عشان نعرف المنتج في المفضلة ولا لأ
  const isFavorite = (productId) => {
    return favoritesItems.some((item) => item.id === productId);
  };

  return (
    <FavoritesContext.Provider
      value={{ favoritesItems, toggleFavorite, isFavorite }}
    >
      {children}
    </FavoritesContext.Provider>
  );
}

// "خطاف" سهل عشان نستخدم المفضلة في أي مكان
export const useFavorites = () => {
  return useContext(FavoritesContext);
};
