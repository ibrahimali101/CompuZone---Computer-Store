import "./BecomeSeller.css";

function BecomeSeller() {
  return (
    <section className="seller-cta-section">
      <div className="seller-cta-container">
        <div className="cta-content">
          <h2>عندك منتجات وعايز تعرضها؟</h2>
          <h1>حوّل شغفك لأرباح وانضم لمنصتنا</h1>
          <p>
            اعرض منتجاتك لآلاف العملاء المهتمين بالتكنولوجيا والجيمنج. نوفر لك
            الأدوات لتبدأ وتنمو تجارتك معنا.
          </p>
          <button className="cta-button">سجل كبائع الآن</button>
        </div>
        <div className="cta-image-container">
          <img
            src="https://images.pexels.com/photos/5632402/pexels-photo-5632402.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            alt="شخص يجهز طلبات للبيع أونلاين"
          />
        </div>
      </div>
    </section>
  );
}

export default BecomeSeller;
