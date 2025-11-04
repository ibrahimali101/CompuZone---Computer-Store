import "./Testimonials.css";
import { FaStar } from "react-icons/fa";

const fakeReviews = [
  {
    id: 1,
    name: "أحمد محمود",
    role: "مشتري معتمد",
    comment:
      "خدمة عملاء ممتازة وسرعة في توصيل الأوردر. تجميعة الكمبيوتر كانت زي ما طلبتها بالظبط واحترافية جداً.",
    imgSrc: "https://randomuser.me/api/portraits/men/32.jpg",
  },
  {
    id: 2,
    name: "سارة علي",
    role: "مشترية معتمدة",
    comment:
      "الموقع سهل جداً في الاستخدام والأسعار تنافسية. لقيت كارت الشاشة اللي كنت بدور عليه بقالي شهور. شكراً CompuZone!",
    imgSrc: "https://randomuser.me/api/portraits/women/44.jpg",
  },
  {
    id: 3,
    name: "محمد إبراهيم",
    role: "جيمنج إنفلونسر",
    comment:
      "طلبت منهم اكسسوارات للسيتاب بتاعي. الجودة كانت أعلى من المتوقع والتغليف كان ممتاز. أنصح بيهم بشدة.",
    imgSrc: "https://randomuser.me/api/portraits/men/46.jpg",
  },
  {
    id: 4,
    name: "نور حسن",
    role: "مستخدمة جديدة",
    comment:
      "تجربة الشراء كانت رائعة! المنتج وصل في ميعاده بالضبط والتعبئة كانت آمنة جداً. أكيد هكرر التجربة تاني ❤️",
    imgSrc: "https://randomuser.me/api/portraits/women/68.jpg",
  },
];


function Testimonials() {
  return (
    <section className="testimonials-section">
      <h2 className="testimonials-title">آراء عملائنا</h2>
      <div className="testimonials-container">
        {fakeReviews.map((review) => (
          <div key={review.id} className="testimonial-card">
            <img
              src={review.imgSrc}
              alt={review.name}
              className="testimonial-image"
            />
            <div className="testimonial-stars">
              <FaStar />
              <FaStar />
              <FaStar />
              <FaStar />
              <FaStar />
            </div>
            <blockquote className="testimonial-comment">
              "{review.comment}"
            </blockquote>
            <p className="testimonial-name">{review.name}</p>
            <span className="testimonial-role">{review.role}</span>
          </div>
        ))}
      </div>
    </section>
  );
}

export default Testimonials;
