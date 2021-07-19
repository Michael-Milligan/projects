#include "bst.h"
#include <exception>

template<class T>
node<T>::node(T data, std::shared_ptr<node<T>>& pLeft, std::shared_ptr<node<T>>& pRight)
{
	this->data = data;
	this->pLeft = pLeft;
	this->pRight = pRight;
}

template<class T>
node<T>::node()
{

}

template<class T>
bst<T>::bst()
{
	root = nullptr;
}

template<class T>
ptr<T> bst<T>::search(T data)
{
	ptr<T> current = std::make_shared<node<T>>(root.get());
	while (current != nullptr)
	{
		T current_data = (*current).data;
		if (data < current_data)
		{
			current = std::make_shared<node<T>>((*current).pLeft.get());
		}
		else if (data > current_data)
		{
			current = std::make_shared<node<T>>((*current).pRight.get());
		}
		else return current;
	}
	throw std::exception("The given element hasn't been found\n");
}

template<class T>
void bst<T>::add(T data)
{
	ptr<T> current = std::make_shared<node<T>>(*root);
	ptr<T> parent = std::make_shared<node<T>>(*root);
	while (current != nullptr)
	{
		T current_data = (*current).data;
		if (data < current_data)
		{
			parent = std::make_shared<node<T>>(current.get());
			current = std::make_shared<node<T>>((*current).pLeft.get());
		}
		else if (data > current_data)
		{
			parent = std::make_shared<node<T>>(current.get());
			current = std::make_shared<node<T>>((*current).pRight.get());
		}
		else throw std::exception("Tree already has element with such data\n");
	}
	ptr<T> pLeft = std::make_shared<node<T>>(nullptr);
	ptr<T> pRight = std::make_shared<node<T>>(nullptr);
	//node<T> current_node(data, pLeft, pRight);
	
}

template<class T>
void bst<T>::output_tree(std::ostream)
{

}


