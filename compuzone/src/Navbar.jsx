import "./Navbar.css";
import { FaHome } from "react-icons/fa";
import { FaShoppingCart, FaSearch, FaHeart } from "react-icons/fa";
import { IoPersonSharp } from "react-icons/io5";
import { NavLink } from "react-router-dom";
function Navbar() {
  return (
    <>
      <div className="navbar">
        <div className="navbar-top">
          <div className="logo-img">
            <img className="logo" src="/CompuZone.png" alt="CompuZone Logo" />
          </div>
          <div className="search-bar">
            <input type="text" placeholder="ابحث عن منتج...." />
            <FaSearch className="search-icon" />
          </div>
          <div className="icons">
            <div className="home-icon">
              <NavLink to="/"><FaHome title="الصفحة الرئيسية" /></NavLink>
            </div>
            <div className="favorites-icon">
              <FaHeart title="المفضلة" />
            </div>
            <div className="cart-icon">
              <NavLink to="/cart"><FaShoppingCart title="عربة التسوق" /></NavLink>
            </div>
            <div className="profile-icon">
              <NavLink to="/login"><IoPersonSharp title="الملف الشخصي" /></NavLink>
            </div>
          </div>
        </div>
        <div className="links">
          <div className="categories">الفئات&nbsp; &nbsp; | </div>
          <ul>
            <li>
              <a href="#">قطع هاردوير</a>
            </li>
            <li>
              <a href="#">تجميعات الكمبيوتر</a>
            </li>
            <li>
              <a href="#">اللاب توب</a>
            </li>
            <li>
              <a href="#">الكونسول</a>
            </li>
            <li>
              <a href="#">الاكسسوارات</a>
            </li>
            <li>
              <a href="#">الشاشات </a>
            </li>
            <li>
              <a href="#">ابني تجميعتك </a>
            </li>
          </ul>
        </div>
        
      </div>
    </>
  );
}
export default Navbar;
