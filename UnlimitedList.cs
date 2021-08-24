using System.Collections.Generic;
using System.Diagnostics;

namespace everywhere
{
    class UnlimitedList
    {
        public object present
        {
            get { return _present.value; }
        }

        public object next
        {
            get { return _present.next.value; }
        }

        public object prev
        {
            get { return _present.prev.value; }
        }

        public decimal Count
        {
            get { return _length; }
        }

        public decimal present_number
        {
            get { return _present_index; }
        }

        public UnlimitedList()
        {
            dummy = new Node();
            _present = dummy;
            _length = 0;
            _present_index = -1;//-1がdummyの証拠
        }

        public UnlimitedList(UnlimitedList ul)
        {
            dummy = new Node();
            //リストに情報をコピーする。
            Node ptr = dummy.next;
            while (ptr != dummy)
            {
                push_back(ptr.value);
            }

        }

        ~UnlimitedList()
        {
            clear();
        }

        //一番前に追加
        public void push_front(object value)
        {
            add(value, dummy);
            _present_index++;
        }

        //一番後ろに追加
        public void push_back(object value)
        {
            add(value, dummy.prev);
        }

        //一番前を削除
        public void pop_front()
        {
            remove(dummy.next);
            _present_index--;
        }

        //一番後ろを削除
        public void pop_back()
        {
            remove(dummy.prev);
        }

        //現在の貴方が認識している地点を前に進める
        public bool is_next()
        {
            Node node = _present.next;
            if (node == dummy) return false;

            _present = node;
            _present_index++;
            return true;
        }

        //現在の貴方が認識している地点を後ろに進める
        public bool is_prev()
        {
            Node node = _present.prev;
            if (node == dummy) return false;

            _present = node;
            _present_index--;
            return true;
        }

        //現在の貴方が認識している地点を一番前にする
        public void come_foremost()
        {
            _present = dummy.next;
            _present_index = 0;
        }

        //現在の貴方が認識している地点を一番後ろにする
        public void come_backmost()
        {
            _present = dummy.prev;
            _present_index = _length - 1;
        }

        //中身を確認する
        public string dump()
        {
            string text = "";

            Node ptr = dummy.next;
            while (ptr != dummy)
            {
                text += ptr.value.ToString() + " / ";
                ptr = ptr.next;
            }
            text += "count " + _length.ToString();
            text += "  present index " + _present_index.ToString();
            return text;
        }

        //全てを削除
        public void clear()
        {
            while (dummy.next != dummy)
            {
                pop_front();
            }
        }

        //リスト内の要素の一つを返却する
        public object get_point(decimal point)
        {
            if (point < 0) return null;
            if (point >= _length) return null;

            Node node = node_point(point);
            return node.value;
        }

        //特定間の双方リストを通常リスト形式で返還する
        public List<object> get_between(decimal last, decimal begin = 0)
        {
            if (begin < 0) return null;
            if (last < 0) return null;
            if (begin >= _length) return null;
            if (last >= _length) return null;
            if (begin >= last) return null;

            Node begin_node = node_point(begin);
            Node last_node = node_point(last);
            
            List<object> data = new List<object>();
            while (begin_node != last_node)
            {
                data.Add(begin_node.value);
                begin_node = begin_node.next;
            }

            return data;
        }


        private Node dummy;             // 内部的に始終を知らせるダミー
        private decimal _length;        // 要素数

        private Node _present;          // 認識している地点
        private decimal _present_index; // 認識している地点の場所

        private void add(object value, Node node)
        {
            Node newNode = new Node(value, node, node.next);
            node.next.prev = newNode;
            node.next = newNode;

            if (_length == 0)
            {
                if (!is_next()) is_prev();
            }
            _length++;
        }

        private void remove(Node node)
        {
            if (dummy.next == dummy) { return; }

            node.prev.next = node.next;
            node.next.prev = node.prev;
            node.value = null;
            node = node.prev;
            _length--;
        }

        private Node node_point(decimal point)
        {
            if (point < 0) return null;
            if (point >= _length) return null;

            if (point == 0) return dummy.next;
            if (point == _length - 1) return dummy.prev;
            if (point == _present_index) return _present;

            decimal direction = 0;
            decimal point_list = 0;
            //ループを最小限に抑える為、開始位置を決定する
            Node p = node_loop_point(point, ref point_list, ref direction);

            while(point != point_list)
            {
                if (direction == 1)
                {
                    p = p.next;
                    point_list++;
                }
                else if(direction == -1)
                {
                    p = p.prev;
                    point_list--;
                }
                else
                {
                    Debug.Assert(false, "An unconfirmed value was detected \"direction\". Please contact the developer.");
                }

                Debug.Assert(p != dummy, "An unexpected error occurred. Please contact the developer.");
            }
            
            return p;
        }

        private Node node_loop_point(decimal point, ref decimal point_list, ref decimal direction)
        {
            // pointが前半にある場合
            if (point <= _present_index)
            {
                decimal p1 = point - (_present_index / 2);
                if (p1 <= 0)
                {
                    direction = 1;
                    point_list = 0;
                    return dummy.next;
                }

                direction = -1;
                point_list = _present_index - 1;
                return _present.prev;
            }

            decimal p2 = point - (((_length - _present_index) / 2) + _present_index);
            // pointが後半にある場合
            if (p2 >= 0)
            {
                direction = -1;
                point_list = _length - 1;
                return dummy.prev;
            }

            direction = 1;
            point_list = _present_index + 1;
            return _present.next;
        }


        private class Node
        {
            public object value;
            public Node next;
            public Node prev;

            public Node()
            {
                value = null;
                next = this;
                prev = this;
            }

            public Node(object value, Node prev, Node next)
            {
                this.value = value;
                this.prev = prev;
                this.next = next;
            }
        }
    }
}
