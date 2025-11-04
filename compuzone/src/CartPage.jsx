import { NavLink } from "react-router-dom";
import "./CartPage.css";
import { FaTrash } from "react-icons/fa";

const fakeCartItems = [
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


function CartPage() {
  const subtotal = fakeCartItems.reduce(
    (total, item) => total + item.newPrice * item.quantity,
    0
  );

  return (
    <div className="cart-page-container">
      <h1>عربة التسوق</h1>

      {fakeCartItems.length === 0 ? (
        <div className="empty-cart">
          <p>عربتك فارغة.</p>
          <NavLink to="/" className="cart-button">
            ابدأ التسوق
          </NavLink>
        </div>
      ) : (
        <div className="cart-content">
          <div className="cart-items-list">
            {fakeCartItems.map((item) => (
              <div key={item.id} className="cart-item">
                <img src={item.imgSrc} alt={item.title} />
                <div className="item-details">
                  <h3>{item.title}</h3>
                  <p>الكمية: {item.quantity}</p>
                  <p>السعر: ج.م {item.newPrice}</p>
                </div>
                <button
                  className="remove-btn"
                  onClick={() => alert("المفروض امسح المنتج ده!")}
                >
                  <FaTrash />
                </button>
              </div>
            ))}
          </div>

          <div className="cart-summary">
            <h2>ملخص الطلب</h2>
            <div className="summary-row">
              <span>الإجمالي الفرعي</span>
              <span>ج.م {subtotal.toFixed(2)}</span>
            </div>
            <div className="summary-row">
              <span>الشحن (مؤقتاً)</span>
              <span>ج.م 50.00</span>
            </div>
            <div className="summary-total">
              <span>الإجمالي</span>
              <span>ج.م {(subtotal + 50).toFixed(2)}</span>
            </div>
            <NavLink to="/checkout" className="cart-button checkout-btn">
              الذهاب لإنهاء الطلب
            </NavLink>
          </div>
        </div>
      )}
    </div>
  );
}

export default CartPage;
