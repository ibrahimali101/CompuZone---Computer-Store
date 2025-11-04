import "./AuthForm.css";
import { FaUser, FaEnvelope, FaLock, FaStore } from "react-icons/fa";

function SellerRegisterForm() {
  return (
    <form className="auth-form">
      <div className="auth-input-group">
        <FaStore className="auth-input-icon" />
        <input
          className="auth-input"
          type="text"
          placeholder="اسم المتجر"
          required
        />
      </div>
      <div className="auth-input-group">
        <FaUser className="auth-input-icon" />
        <input
          className="auth-input"
          type="text"
          placeholder="الاسم بالكامل"
          required
        />
      </div>
      <div className="auth-input-group">
        <FaEnvelope className="auth-input-icon" />
        <input
          className="auth-input"
          type="email"
          placeholder="البريد الإلكتروني"
          required
        />
      </div>
      <div className="auth-input-group">
        <FaLock className="auth-input-icon" />
        <input
          className="auth-input"
          type="password"
          placeholder="كلمة المرور"
          required
        />
      </div>
      <button type="submit" className="auth-button">
        إنشاء حساب بائع
      </button>
    </form>
  );
}

export default SellerRegisterForm;
