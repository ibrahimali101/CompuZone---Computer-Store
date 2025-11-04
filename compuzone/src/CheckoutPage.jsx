import "./CheckoutPage.css";
import {
  FaUser,
  FaMapMarkerAlt,
  FaPhone,
  FaCreditCard,
  FaCalendarAlt,
  FaLock,
} from "react-icons/fa";

function CheckoutPage() {
  const orderSummaryItems = [
    {
      id: 1,
      imgSrc: "https://picsum.photos/id/180/400/300",
      title: "لابتوب جيمنج G15 RAM 16GB RTX 4060",
      newPrice: "32500",
      quantity: 1,
    },
    {
      id: 2,
      imgSrc: "https://picsum.photos/id/27/400/300",
      title: "شاشة سامسونج 27 بوصة 144Hz",
      newPrice: "7100",
      quantity: 2,
    },
  ];

  const subtotal = orderSummaryItems.reduce(
    (total, item) => total + item.newPrice * item.quantity,
    0
  );

  return (
    <div className="checkout-container">
      <div className="checkout-form-container">
        <h1>إنهاء الطلب</h1>

        <form className="checkout-form">
          <section className="form-section">
            <h2>1. عنوان الشحن</h2>
            <div className="form-group">
              <label htmlFor="name">الاسم بالكامل</label>
              <div className="input-with-icon">
                <input type="text" id="name" required />
                <FaUser className="input-icon" />
              </div>
            </div>
            <div className="form-group">
              <label htmlFor="address">العنوان بالتفصيل</label>
              <div className="input-with-icon">
                <input type="text" id="address" required />
                <FaMapMarkerAlt className="input-icon" />
              </div>
            </div>
            <div className="form-group">
              <label htmlFor="phone">رقم الهاتف</label>
              <div className="input-with-icon">
                <input type="tel" id="phone" required />
                <FaPhone className="input-icon" />
              </div>
            </div>
          </section>
          <section className="form-section">
            <h2>2. معلومات الدفع</h2>
            <div className="form-group">
              <label htmlFor="card-name">الاسم على الكارت</label>
              <div className="input-with-icon">
                <input type="text" id="card-name" required />
                <FaUser className="input-icon" />
              </div>
            </div>
            <div className="form-group">
              <label htmlFor="card-number">رقم الكارت</label>
              <div className="input-with-icon">
                <input
                  type="text"
                  id="card-number"
                  placeholder="XXXX XXXX XXXX XXXX"
                  required
                />
                <FaCreditCard className="input-icon" />
              </div>
            </div>
            <div className="form-row">
              <div className="form-group">
                <label htmlFor="expiry">تاريخ الانتهاء</label>
                <div className="input-with-icon">
                  <input type="text" id="expiry" placeholder="MM/YY" required />
                  <FaCalendarAlt className="input-icon" />
                </div>
              </div>
              <div className="form-group">
                <label htmlFor="cvc">CVC</label>
                <div className="input-with-icon">
                  <input type="text" id="cvc" placeholder="123" required />
                  <FaLock className="input-icon" />
                </div>
              </div>
            </div>
          </section>

          <button type="submit" className="checkout-button">
            إتمام الدفع (ج.م {(subtotal + 50).toFixed(2)})
          </button>
        </form>
      </div>
      <div className="order-summary-container">
        <h2>ملخص الطلب</h2>
        <div className="summary-items">
          {orderSummaryItems.map((item) => (
            <div key={item.id} className="summary-item">
              <img src={item.imgSrc} alt={item.title} />
              <div className="summary-item-details">
                <span className="summary-item-title">
                  {item.title} (x{item.quantity})
                </span>
                <span className="summary-item-price">ج.م {item.newPrice}</span>
              </div>
            </div>
          ))}
        </div>
        <div className="summary-totals">
          <div className="summary-row">
            <span>الإجمالي الفرعي</span>
            <span>ج.م {subtotal.toFixed(2)}</span>
          </div>
          <div className="summary-row">
            <span>الشحن</span>
            <span>ج.م 50.00</span>
          </div>
          <div className="summary-total">
            <span>الإجمالي</span>
            <span>ج.م {(subtotal + 50).toFixed(2)}</span>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CheckoutPage;
