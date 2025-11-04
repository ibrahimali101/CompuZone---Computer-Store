import "./AuthForm.css";
import { FaUser, FaEnvelope, FaLock } from "react-icons/fa";

function CustomerRegisterForm() {
  return (
    <form className="auth-form">
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
      <div className="auth-input-group">
        <FaLock className="auth-input-icon" />
        <input
          className="auth-input"
          type="password"
          placeholder="تأكيد كلمة المرور"
          required
        />
      </div>
      <button type="submit" className="auth-button">
        إنشاء حساب مستخدم
      </button>
    </form>
  );
}

export default CustomerRegisterForm;
