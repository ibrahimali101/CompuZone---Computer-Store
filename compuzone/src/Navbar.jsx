import "./Navbar.css";
import { FaHome } from "react-icons/fa";
import { FaShoppingCart, FaSearch } from "react-icons/fa";
import { IoPersonSharp } from "react-icons/io5";
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
            <div className="profile">
              <div className="profile-icon">
                <IoPersonSharp />
              </div>
              <div className="auth-links">
                <a href="#">تسجيل الدخول</a><span>/</span><a href="#">إنشاء حساب</a>
              </div>
            </div>
          </div>
        </div>
        <div className="links">
          <div className="categories">الفئات&nbsp; &nbsp; |  </div>
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
              {/* could be canceled */}
            </li> 
          </ul>
        </div>
      </div>
    </>
  );
}
export default Navbar;
