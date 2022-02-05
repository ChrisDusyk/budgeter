import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, Legend } from "recharts";

const data = [
	{ name: "Pizza", amount: 39.99 },
	{ name: "J&A", amount: 68.89 },
	{ name: "Bomb", amount: 80.94 },
];

export function FirstChart() {
	return (
		<>
			<LineChart width={600} height={400} data={data}>
				<Line type="monotone" dataKey="amount" stroke="#8884d8" />
				<CartesianGrid stroke="#ccc" />
				<XAxis dataKey="name" />
				<YAxis />
				<Tooltip />
				<Legend />
			</LineChart>
		</>
	);
}
