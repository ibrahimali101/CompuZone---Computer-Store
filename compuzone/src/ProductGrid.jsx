import ProductCard from "./ProductCard";

// 1. هنا التعديل: لازم تستقبل "products" كـ prop
function ProductGrid({ products }) {
  return (
    <div className="product-grid">
      {/* 2. هنا التعديل: بنلف على الـ "products" اللي استقبلناها */}
      {products.map((product) => (
        <ProductCard key={product.id} product={product} />
      ))}
    </div>
  );
}

export default ProductGrid;
