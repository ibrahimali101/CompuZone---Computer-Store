import "./Navbar.css";
import { FaHome } from "react-icons/fa";
import { FaShoppingCart, FaSearch } from "react-icons/fa";
import { IoPersonCircle } from "react-icons/io5";
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
              <FaHome />
            </div>
            <div className="cart-icon">
              <FaShoppingCart />
            </div>
            <div className="profile-icon">
              <span>
                <IoPersonCircle />
                <a href="#">تسجيل الدخول</a> | <a href="#">إنشاء حساب</a>
              </span>
            </div>
          </div>
        </div>
        <div className="links">
          <span className="categories">الفئات |</span>
          <ul>
            <li>
              <a href="#">قطع الكمبيوتر</a>
            </li>
            <li>
              <a href="#">تجميعات كمبيوتر</a>
            </li>
            <li>
              <a href="#">اللاب توب</a>
            </li>
            <li>
              <a href="#">الاكسسوارات</a>
            </li>
          </ul>
        </div>
      </div>
    </>
  );
}
export default Navbar;
