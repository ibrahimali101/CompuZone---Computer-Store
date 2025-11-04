import "./AuthForm.css";
import { NavLink } from "react-router-dom";
import { FaEnvelope, FaLock } from "react-icons/fa";

function LoginPage() {
  return (
    <div className="auth-container">
      <div className="auth-form-box">
        <h1>تسجيل الدخول</h1>
        <form className="auth-form">
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
            دخول
          </button>
        </form>
        <p className="auth-switch-link">
          مستخدم جديد؟ <NavLink to="/register">إنشاء حساب</NavLink>
        </p>
      </div>
    </div>
  );
}

export default LoginPage;
