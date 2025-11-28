import Navbar from "./Navbar";
import "./App.css";
import HeroSection from "./HeroSection";
import BestSellers from "./BestSellers";
import BecomeSeller from "./BecomeSeller";
import Testimonials from "./Testimonials";
import Footer from "./Footer";
import LoginPage from "./LoginPage";
import RegisterPage from "./RegisterPage";
import CartPage from "./CartPage";
import CheckoutPage from "./CheckoutPage";
import ProductsPage from "./ProductsPage";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import { FavoritesProvider } from "./FavoritesContext"; // <-- 1. استدعي المخزن
import FavoritesPage from "./FavoritesPage"; // <-- 2. استدعي الصفحة الجديدة

function App() {
  return (
    <>
      <BrowserRouter>
        <FavoritesProvider>
          {" "}
          {/* <-- 3. غلف كل حاجة بالمخزن */}
          <Navbar />
          <Routes>
            <Route
              path="/"
              element={
                <>
                  <HeroSection />
                  <BestSellers />
                  <BecomeSeller />
                  <Testimonials />
                </>
              }
            />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/cart" element={<CartPage />} />
            <Route path="/cart/checkout" element={<CheckoutPage />} />
            <Route path="/products" element={<ProductsPage />} />
            <Route path="/favorites" element={<FavoritesPage />} />{" "}
            {/* <-- 4. ضيف الراوت ده */}
          </Routes>
          <Footer />
        </FavoritesProvider>{" "}
        {/* <-- 5. اقفل التغليف */}
      </BrowserRouter>
    </>
  );
}

export default App;
