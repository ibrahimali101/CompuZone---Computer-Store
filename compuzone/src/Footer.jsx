import "./Footer.css";
import { FaFacebook, FaTwitter, FaInstagram, FaLinkedin } from "react-icons/fa";

function Footer() {
  return (
    <footer className="footer">
      <div className="footer-container">
        <div className="footer-column">
          <img
            className="footer-logo"
            src="/CompuZone Black.jpg"
            alt="CompuZone Logo"
          />
          <p>
            CompuZone هو وجهتك الأولى لكل ما يخص عالم الكمبيوتر والجيمنج في مصر.
            نوفر أحدث القطع والتجميعات بأفضل الأسعار.
          </p>
        </div>
        <div className="footer-column">
          <h4>روابط سريعة</h4>
          <ul>
            <li>
              <a href="#">الرئيسية</a>
            </li>
            <li>
              <a href="#">جميع المنتجات</a>
            </li>
            <li>
              <a href="#">افضل العروض</a>
            </li>
            <li>
              <a href="#">ابني تجميعتك</a>
            </li>
          </ul>
        </div>
        <div className="footer-column">
          <h4>الدعم والمساعدة</h4>
          <ul>
            <li>
              <a href="#">الأسئلة الشائعة</a>
            </li>
            <li>
              <a href="#">سياسة الاسترجاع</a>
            </li>
            <li>
              <a href="#">تتبع طلبك</a>
            </li>
            <li>
              <a href="#">اتصل بنا</a>
            </li>
          </ul>
        </div>
        <div className="footer-column">
          <h4>تواصل معنا</h4>
          <p>تابعنا على شبكات التواصل الاجتماعي</p>
          <div className="footer-social-icons">
            <a href="#" title="Facebook">
              <FaFacebook />
            </a>
            <a href="#" title="Twitter">
              <FaTwitter />
            </a>
            <a href="#" title="Instagram">
              <FaInstagram />
            </a>
          </div>
        </div>
      </div>
      <div className="footer-bottom">
        <p>&copy; {new Date().getFullYear()} CompuZone. جميع الحقوق محفوظة.</p>
      </div>
    </footer>
  );
}

export default Footer;
