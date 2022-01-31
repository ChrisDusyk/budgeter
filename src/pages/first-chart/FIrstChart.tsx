import { LineChart, Line, CartesianGrid, XAxis, YAxis } from "recharts";
const data = [
	{ name: "Page A", uv: 400, pv: 2400, amt: 2400 },
	{ name: "Page B", uv: 350, pv: 2500, amt: 2500 },
	{ name: "Page C", uv: 200, pv: 1000, amt: 1500 },
];

export function FirstChart() {
	return (
		<LineChart width={600} height={400} data={data}>
			<Line type="monotone" dataKey="uv" stroke="#8884d8" />
			<CartesianGrid stroke="#ccc" />
			<XAxis dataKey="name" />
			<YAxis />
		</LineChart>
	);
}
