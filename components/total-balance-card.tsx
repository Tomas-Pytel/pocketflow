export default function TotalBalanceCard() {
  return (
    <div className="relative bg-orange-500 rounded-xl shadow p-6 w-full overflow-hidden">
      <div className="absolute -top-10 -right-10 w-32 h-32 bg-orange-400 rounded-full"></div>
      <h2 className="text-sm font-normal text-gray-100 mb-4">Total Balance</h2>
      <div className="text-3xl font-bold text-white">$12,345.67</div>
      <p className="text-sm text-gray-500 mt-2">As of June 2024</p>
    </div>
  );
}
