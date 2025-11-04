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
import { BrowserRouter, Route, Routes } from "react-router-dom";

function App() {
  return (
    <>
      <BrowserRouter>
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
          <Route path="/checkout" element={<CheckoutPage />} />
        </Routes>
        <Footer />
      </BrowserRouter>
    </>
  );
}

export default App;
