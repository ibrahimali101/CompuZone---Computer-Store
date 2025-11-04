import { useState } from "react";
import "./AuthForm.css";
import { NavLink } from "react-router-dom";
import CustomerRegisterForm from "./CustomerRegisterForm.jsx";
import SellerRegisterForm from "./SellerRegisterForm.jsx";

function RegisterPage() {
  const [formType, setFormType] = useState(null); 

  const renderForm = () => {
    if (formType === "customer") {
      return <CustomerRegisterForm />;
    }
    if (formType === "seller") {
      return <SellerRegisterForm />;
    }
    return (
      <div className="register-choice-box">
        <button
          className="choice-button customer"
          onClick={() => setFormType("customer")}
        >
          تسجيل كمستخدم
        </button>
        <button
          className="choice-button seller"
          onClick={() => setFormType("seller")}
        >
          تسجيل كبائع
        </button>
      </div>
    );
  };

  return (
    <div className="auth-container">
      <div className="auth-form-box">
        <h1>
          {formType === null
            ? "اختر نوع الحساب"
            : formType === "customer"
            ? "حساب مستخدم جديد"
            : "حساب بائع جديد"}
        </h1>

        {renderForm()}

        <p className="auth-switch-link">
          لديك حساب بالفعل؟ <NavLink to="/login">تسجيل الدخول</NavLink>
        </p>
      </div>
    </div>
  );
}

export default RegisterPage;
